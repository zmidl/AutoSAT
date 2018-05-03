using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;  // DllImport,MarshalAs,GCHandle
using System.Text;
using System.Windows;
using System.Xml;

namespace RDCM
{
    public enum LogType
    {
        Info = 0,	// 通常信息，如正常完成的
        Warn = 1,	// 报错提示
        Error = 2,	// 报错提示
        Debug = 3,	// 用于程序调试信息，输出调用函数参数值以及结果值
        Trace = 4,	// 调试用的跟终信息      
        Unknow = 5	// 不知道是啥错误
    };

    public enum PackedUserFlag
    {
        PUF_CodeUTF8 = 0x00000000,	// Utf8编码
        PUF_CodeANSI = 0x00010000,	// ANSI编码
        PUF_CodeUnicode = 0x00020000,	// Unicode编码
        PUF_DataXML = 0x00000000,	// XML
        PUF_DataStr = 0x01000000,	// String
        PUF_DataBin = 0x02000000,	// Bin 
        PUF_UserDataMask = 0x0000FFFF	// 用户数据掩码
    };

    //设备类型,值为设备ID标识是为CAN节点的ID
    public enum EDeviceType { Reader = 0x81, Incubation = 0x82, Shaker = 0x83, Shell = 0x84 };

    // 仪器外置设备DEV_Shell上的开关:灯,锁,温湿计通道,温湿计的采样隔,值是CAN通信中的标识
    public enum EShellType { Lamp = 0x10, Lock = 0x11, ThyChannel = 0x12, ThyInterval = 0x13 };

    public class RecivePacked
    {
        public RecivePacked(string strModule, string strPacked)
        {
            ReciveModule = strModule;
            PackedName = strPacked;

        }
        public string ReciveModule { get; set; }
        public string PackedName { get; set; }
    }

    public class CListenPacked
    {
        public string ReciveModule { get; set; }
        public string PackedName { get; set; }
    }

    public class Module
    {
        //public static UInt32 nSendTimeout = 1000;
        //public static UInt32 nWaitTimeout = 1000;
        //---------------------------------
        public List<RecivePacked> ListenPacked;
        public bool IsInit { get; set; }

        public string ModuleName { get; set; }

        public Module(string strModuleName) // 构造函数
        {
            ModuleName = strModuleName;
            ListenPacked = new List<RecivePacked>();
            M_xmlConfig = new XmlDocument();
        }
        ~Module() // 析构函数
        {

            string strMsg = string.Format("退出{0},CModuleBase::~CModuleBase()", this.ModuleName);
            PostLog(strMsg, LogType.Info);
            if (IsInit)
            {
                ErrCode ret = ErrCode.EC_OK;
                ret = CShareDll.UnRegister(ModuleName);
                IsInit = false;
            };
        }
        public XmlDocument M_xmlConfig { get; set; }


        public virtual ErrCode Init(object objCallback)
        {
            ErrCode retCode = ErrCode.EC_OK;
            if (IsInit) return retCode;
            //-----------------------------------
            IntPtr intPtr = IntPtr.Zero;
            //GCHandle gcHandle = GCHandle.Alloc(this, GCHandleType.Pinned);
            //intPtr = gcHandle.AddrOfPinnedObject();
            object obj = IntPtr.Zero;

            retCode = CShareDll.RegisterService(ModuleName, new ReceiveCallback(OnRecive), objCallback);//

            string strMsg;
            strMsg = String.Format("{0}=CBaseModule.Init({01})", retCode, objCallback.ToString());
            PostLog(strMsg, LogType.Info);
            if (retCode == ErrCode.EC_OK) IsInit = true;
            //--------------
            string strFileName;
            string strDir = System.AppDomain.CurrentDomain.BaseDirectory;
            strFileName = String.Format("{0}Config\\RdCalcQuant.xml", strDir);
            try
            {
                M_xmlConfig.Load(strFileName);
            }
            catch (Exception e)
            {
                strMsg = e.ToString();
                PostLog(strMsg, LogType.Error);
            }
            strMsg = String.Format("启动{0}{1}", this.ModuleName, retCode == ErrCode.EC_OK ? "成功" : "失败"); ;
            PostLog(strMsg, LogType.Info);
            return retCode;

        }

        public virtual bool InitCmds()
        {

            XmlNode xmlInitCmds;

            xmlInitCmds = M_xmlConfig.SelectSingleNode("//Config/InitCmds");//查找<Employees> 
            if (xmlInitCmds != null)
            {
                foreach (XmlNode xmlInitCmd in xmlInitCmds)
                {
                    this.DoRecive((XmlElement)xmlInitCmd, this.ModuleName, null);
                }
            }
            return true;
        }
        public virtual bool ExitCmds()
        {
            XmlNode xmlInitCmds;

            xmlInitCmds = M_xmlConfig.SelectSingleNode("//Config/ExitCmds");//查找<Employees> 
            if (xmlInitCmds != null)
            {
                foreach (XmlNode xmlInitCmd in xmlInitCmds)
                {
                    this.DoRecive((XmlElement)xmlInitCmd, this.ModuleName, null);
                }
            }
            return true;
        }
        int DoRegisterListenEvent(XmlElement xmlInvoke, string strForm)
        {
            //<RegisterListen  bMustReturn="1" strReciveModule="FlourReader" strPackedName="CycleFluor" bSet="1" />
            string strReciveModule = xmlInvoke.GetAttribute("strReciveModule");
            string strPackedName = xmlInvoke.GetAttribute("strPackedName");
            string bSet = xmlInvoke.GetAttribute("bSet");
            if (strReciveModule.Length == 0 || strPackedName.Length == 0) return 0;
            for (int i = 0; i < ListenPacked.Count; i++)
            {
                if (ListenPacked[i].ReciveModule.CompareTo(strReciveModule) == 0
                    && ListenPacked[i].PackedName.CompareTo(strPackedName) == 0)
                {
                    if (bSet.CompareTo("0") == 0) ListenPacked.RemoveAt(i);
                    return 0; //找到已注册过
                }
            }
            ListenPacked.Add(new RecivePacked(strReciveModule, strPackedName));
            return 0;
        }

        public void PostLog(string strMsg, LogType nType)
        {
            UInt32 nUserFlag = (UInt32)(PackedUserFlag.PUF_CodeUTF8 | PackedUserFlag.PUF_DataXML);
            //EnumSendBack(strMsg, "PostLog", nUserFlag);
            DateTime dt = DateTime.Now;
            //sprintf_s(szCreateTime,nLen,"%04d-%02d-%02d %02d:%02d:%02d" ,st.wYear,st.wMonth,st.wDay,st.wHour,st.wMinute,st.wSecond);
            string strPack;

            strPack = string.Format("<PostLog strModuleName=\"{0}\" strTime=\"{1:HH:mm:ss.fff}\" nLogType=\"{2}\" > \"{3}\" </PostLog>",
                this.ModuleName, dt, Convert.ToInt32(nType), strMsg);
            ErrCode nRet = 0;
            int nCount = 0;
            for (int i = 0; i < ListenPacked.Count; i++)
            {
                if (ListenPacked[i].PackedName.CompareTo("PostLog") == 0)
                {
                    nRet = SendXmlElement(strPack, nUserFlag, this.ModuleName, ListenPacked[i].ReciveModule);
                    if (nRet == ErrCode.EC_OK) nCount++;
                }
            }
            if (ListenPacked.Count == 0) nRet = SendXmlElement(strPack, nUserFlag, this.ModuleName, "Module_Log_Service");
        }
        // 处理收到的数据
        public virtual int DoRecive(XmlElement xmlElement, string strForm, Object objCallback)
        {
            int nRet = 0;
            string strPackName = xmlElement.Name;
            if (0 == strPackName.CompareTo("CloseApp") && objCallback != null)
            {
                if (objCallback is Window wnd)
                {
                    wnd.Dispatcher.Invoke(() =>
                    {
                        wnd.Close();
                    });
                }
            }
            else if (0 == strPackName.CompareTo("ListenEvent"))
            {
                DoRegisterListenEvent(xmlElement, strForm);
            }
            else if (0 == strPackName.CompareTo("BatchCmds"))
            {
                DoBatchCmds(xmlElement);
            }
            else if (0 == strPackName.CompareTo("BatchCmd"))
            {
                DoBatchCmd(xmlElement);
            }
            else
            {
                string strMsg = String.Format("无该包数据的处理方法,DoRecive(strForm=\"{0}\",strPackName=\"{1})\"", strForm, strPackName); ;
                PostLog(strMsg, LogType.Error);

            }
            return nRet;
        }
        public virtual int DoRecive(string strData, UInt32 nUserFlag, string strForm, string strTo, Object objCallback)
        {
            int nRet = 0;

            XmlDocument xmlDoc = new XmlDocument();
            try
            {

                xmlDoc.LoadXml(strData);
            }
            catch (Exception e)
            {
                // 输出误信息,以便于调试
                string strMsg = e.ToString();
                System.Diagnostics.Debug.WriteLine(strMsg);
                string strRet = String.Format("<Return  strMsg=\"{0}\"/>", e.ToString());
                CShareDll.SendData(strRet, nUserFlag, strTo, strForm, 100);
                PostLog(strMsg, LogType.Error);

                return nRet;
            }
            nRet = DoRecive(xmlDoc.DocumentElement, strForm, objCallback);

            return nRet;
        }
        // 需使用静态函数才可以,否则在DLL中回调些函数时,随机几次后,就使用失败了
        protected virtual int OnRecive(IntPtr pData, UInt32 nLen, UInt32 nUserFlag, string strForm, string strTo, IntPtr objCallbackIntPtr)
        {
            Object objCallback = null;
            if (objCallbackIntPtr != IntPtr.Zero)
            {
                GCHandle gch = (GCHandle)objCallbackIntPtr;
                objCallback = gch.Target;
            }

            int nRet = 0;
            // 收到数据,由UT8转为字符串
            byte[] bufRecive = new byte[nLen];
            Marshal.Copy(pData, bufRecive, 0, (int)nLen);
            //byte[] bytes = Encoding.Convert(Encoding.UTF8, Encoding.Default, bufRecive);
            // string strRecive = Encoding.Default.GetString(bufRecive);
            string strRecive = Encoding.UTF8.GetString(bufRecive);
            nRet = DoRecive(strRecive, nUserFlag, strForm, strTo, objCallback);
            bufRecive = null;

            return nRet;
        }
        public int EnumSendBack(XmlElement pXmlData, string szPackedName)
        {
            UInt32 nUserFlag = (UInt32)(PackedUserFlag.PUF_CodeUTF8 | PackedUserFlag.PUF_DataXML);
            return EnumSendBack(pXmlData.OuterXml, szPackedName, nUserFlag);
        }
        public int EnumSendBack(string strSend, string szPackedName, UInt32 nUserFlag)
        {
            int nCount = 0;
            ErrCode nRet = 0;
            string strMsg;
            string strOk = "";
            string strErr = "";
            List<int> listSend = new List<int>(ListenPacked.Count);

            for (int i = 0; i < ListenPacked.Count; i++)
            {
                if (ListenPacked[i].PackedName.CompareTo(szPackedName) == 0)
                {
                    nRet = SendXmlElement(strSend, nUserFlag, this.ModuleName, ListenPacked[i].ReciveModule);
                    if (nRet == ErrCode.EC_OK)
                    {
                        strOk += String.Format("{0},", ListenPacked[i].ReciveModule);
                        nCount++;
                    }
                    else
                    {
                        strErr += String.Format("{0},", ListenPacked[i].ReciveModule);
                    }

                }
                //m_clsShareDll.SendXmlData(pXmlData,0,this->m_strModuleName.GetData(),m_vctListenPacked[i]->strReciveModule.c_str() ,m_dwSendTimeout);
            }
            if (strOk.Length == 0 && strErr.Length == 0) return nCount;

            strMsg = String.Format("发送{0}包,", szPackedName);
            if (strOk.Length > 0)
            {
                strMsg += String.Format("成功:{0}", strOk);
            }
            if (strErr.Length > 0)
            {
                strMsg += String.Format("失败:{0}", strErr);
            }
            PostLog(strMsg, LogType.Info);

            return nCount;
        }

        public int DoBatchCmds(XmlElement xmlBatchCmds)
        {

            //<BatchCmds>
            //  <BatchCmd strFrom=""  strTo="Module_Reader_Service" strMemo="启动时自动初始化设置">
            //    <ListenEvent    strReciveModule="Module_RDS_Service" strPackedName="NapReadCompleted"  bSet="1"  />

            if (xmlBatchCmds.Name.CompareTo("BatchCmds") != 0) return 0;
            foreach (XmlElement xmlBatchCmd in xmlBatchCmds)
            {
                DoBatchCmd(xmlBatchCmd);
            }
            return 0;

        }
        public int DoBatchCmd(XmlElement xmlBatchCmd)
        {
            string strFrom, strTo;
            int nCount = 0;
            ErrCode nRet;
            if (xmlBatchCmd.Name.CompareTo("BatchCmd") != 0) return 0;

            strFrom = xmlBatchCmd.GetAttribute("strFrom");
            strTo = xmlBatchCmd.GetAttribute("strTo");
            foreach (XmlElement xmlCmd in xmlBatchCmd)
            {
                if (xmlCmd.Name.CompareTo("Sleep") == 0)  //  
                {
                    int nMillisecond = 5;
                    if (xmlCmd.GetAttribute("nMillisecond") != null)
                        nMillisecond = Convert.ToInt32(xmlCmd.GetAttribute("nMillisecond"));

                    System.Threading.Thread.Sleep(nMillisecond); //20170731,底层硬件可能来不及处理,所以暂时要加延时.
                    continue;
                }
                if (strTo.CompareTo(this.ModuleName) == 0)
                {
                    DoRecive(xmlCmd, strFrom, null);
                }
                else
                {

                    nRet = SendXmlElement(xmlCmd, 0, strFrom, strTo);
                    if (nRet == ErrCode.EC_OK) nCount++;
                }
            }
            return 0;



        }
        ////设置振动模块的开关
        //public ErrCode Shake(bool bActive,int nSpeed, int nDelay, int nDirect)
        //{
        //    ErrCode retCode = ErrCode.EC_OK;
        //    string strTo = strRdCanServiceName;
        //    //<Shaker  bMustReturn="1"  strCommand="Active" bSet="1" nSpeed="1000" nDelay="50" nDirection="0" />
        //    //"<Shaker bMustReturn=\"1\"  strCommand=\"Active\" nSpeed=\"1000\" nDelay=\"5\" nDirection=\"0\" />",
        //    string strSend = String.Format("<Shaker  bMustReturn=\"1\"  strCommand=\"Active\" bSet=\"{0}\"  nSpeed=\"{1}\" nDelay=\"{2}\" nDirection=\"{3}\" />",
        //        bActive?1:0, nSpeed, nDelay, nDirect);
        //     UInt32 nUserFlag = 0;
        //     retCode = CShareDll.SendData(strSend, nUserFlag, strRdsClientName, strTo, nSendTimeout);
        //     return retCode;           
        //}
        // 发送XmlElement到服务端 
        public ErrCode SendXmlElement(XmlElement xmlElement, UInt32 nUserFlag, string strFrom, string strTo)
        {
            return SendXmlElement(xmlElement.OuterXml, nUserFlag, strFrom, strTo);

        }
        public ErrCode SendXmlElement(string strSend, UInt32 nUserFlag, string strFrom, string strTo)
        {
            ErrCode retCode = ErrCode.EC_OK;
            retCode = CShareDll.SendData(strSend, nUserFlag, strFrom, strTo, 1000);
            return retCode;
        }
        // 等待服务端返回XmlElement
        public XmlElement WaitXmlElement(out string strFrom, out UInt32 nUserFlag, UInt32 nWaitTimeout)
        {
            XmlElement retXmlElement = null;
            ErrCode retCode = ErrCode.EC_OK;
            string strRecve = string.Empty;

            retCode = CShareDll.WaitData(out strRecve, out nUserFlag, out strFrom, ModuleName, nWaitTimeout);
            if (retCode != ErrCode.EC_OK)
            {
                //string strMsg = String.Format("等待消息超时,WaitData(ReciveModuleName=\"{0}\",nTimeout={1})",ModuleName,nWaitTimeout); ;
                //PostLog(strMsg, LogType.LT_TRACE);
                return retXmlElement;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strRecve);
            retXmlElement = xmlDoc.DocumentElement;
            return retXmlElement;
        }
    }
}