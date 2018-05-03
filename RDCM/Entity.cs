using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RDCM
{

    #region  上位机与读数模块的通信类
    //Module_RDS与Module_Reader通讯协议
    //主程序端
    //Module_RDS_Client
    //Module_RDS_Service

    //读数端
    //Module_Reader_Client
    //Module_Reader_Service
    //XML编码格式:UTF8

    // 基本上都用到bMustReturn
    public class CCommBase
    {
        [XmlAttribute("bMustReturn")]
        public int bMustReturn { get; set; }
        public CCommBase()
        {
            bMustReturn = 1;
        }
    }

    //<Return strName="Shaker"   nResult="0"  />
    [XmlRoot("Return")]
    public class CReturn : CCommBase
    {
        [XmlAttribute("strName")]
        public string strName { get; set; }
        [XmlAttribute("nResult")]
        public int nResult { get; set; }
        public CReturn()
        {
            bMustReturn = 0;
        }
    }
    // 六联排读完后的事件,为及时搬运,因计算结果再返回会有有延时,时间不定
    //<NapReadCompleted  bMustReturn="1" >	
    //    <Nap nID="1"   nPos="1" / >		
    //    <Nap nID="2"   nPos="1" / >
    // </NapReadCompleted>
    [XmlRoot("NapReadCompleted")]
    public class CNapReadCompleted : CFluroData
    {

    }
    //  设置六联排已加酶,可以多个六联排
    //<SetNaps   bMustReturn="1" >	
    //    <Nap nID="1"   nCurrentPos="1"  >		
    //       <Cell  nPos="1" strItemName="UU" strEnzymeTime="2017-05-09 23:05:57" / >
    // </Nap>
    //</SetNaps>
    //结构与FluroData一样,可以直接继续承
    [XmlRoot("SetNaps")]
    public class CSetNaps : CFluroData
    {

    }

    //关闭对象程序
    //<CloseApp    /> 
    [XmlRoot("CloseApp")]
    public class CCloseApp : CCommBase
    {
    }

    //振动模块

    //<Shaker  bMustReturn="1" strCommand="Active" bSet="1" nSpeed="1000" nDelay="50" nDirection="0" />
    [XmlRoot("Shaker")]
    public class CShaker : CCommBase
    {

        [XmlAttribute("strCommand")]
        public string strCommand { get; set; }
        [XmlAttribute("bSet")]
        public int bSet { get; set; }
        [XmlAttribute("nSpeed")]
        public int nSpeed { get; set; }
        [XmlAttribute("nDelay")]
        public int nDelay { get; set; }
        [XmlAttribute("nDirection")]
        public int nDirection { get; set; }
        public CShaker()
        {
            strCommand = "Active";
            bSet = 1;
        }
    }



    ////读数模板返回读数结果,加酶时间为时间字符串
    //<FluroData  bMustReturn="1">
    //  <Nap nID="1"   nCurrentPos="1"  >		
    //   <Cell  nPos="1" strItemName="UU" strEnzymeTime="2017-05-09 23:05:57" >
    //       <Result nCycleCount="40"  fCt="23.5" fConc="8976516978"  nResult="" >
    //          <Channel1  strTime="" strRaw="" strValue="" />
    //         <Channel2  strTime="" strRaw="" strValue="" />      
    //      </Result>  
    //     </Cell>
    // </Nap>	
    // <Nap strMemo="很多个" />	 
    //</Event>
    [XmlRoot("FluroData")]
    public class CFluroData : CCommBase
    {
        [XmlElement("Nap")]
        public List<CNap> Naps;
        public CFluroData()
        {
            Naps = new List<CNap>();
        }
    }

    //仪器外壳设备,以nType定义枚常量,区分是什么设备,
    //<Shell  strCommand="Active|Check|" nType="16" nChannel="0" bSet="1" />
    [XmlRoot("Shell")]
    public class CShell : CCommBase
    {
        [XmlAttribute("strCommand")]
        public string strCommand { get; set; }
        [XmlAttribute("nType")]
        public int nType { get; set; }
        [XmlAttribute("nChannel")]
        public int nChannel { get; set; }
        [XmlAttribute("bSet")]
        public int bSet { get; set; }
        public CShell()
        {
            strCommand = "Active";
            bSet = 1;
        }
    }
    //以nDeviceID定义枚常量,区分是什么模块,
    //<Temprature  nDeviceID="129" nTempr="420"  nTargetTempr="420" nInTempr="300" /> // 发命令读读数模块温度时返回
    //<Temprature  nDeviceID="129" nTempr="420"  nInTempr="300" />  // 返回荧光数据时返回
    //<Temprature  nDeviceID="130" nTempr="420"  nInTempr="300" />  // 加热模块返回

    [XmlRoot("Temperature")]
    public class CTemprature : CCommBase
    {
        [XmlAttribute("nDeviceID")]
        public int nDeviceID { get; set; }// CanID:0x81读数模块, CanID:0x82温浴模块温度
        [XmlAttribute("nTempr")]
        public int nTempr { get; set; }// 读数模块/温浴模块温度,如果是发送则是设置温度
        [XmlAttribute("nTargetTempr")]
        public int nTargetTempr { get; set; }// 设置或返回读数模块的目标温度        
        [XmlAttribute("nInTempr")]
        public int nInTempr { get; set; } // 读数模块环境温度

    }

    //温湿度返回数据Hygrothermograph Data
    //<ThyData  bMustReturn="0" nChannel="1" nTempr="2505"  nHumidity="7821">
    [XmlRoot("ThyData")]
    public class CThyData : CCommBase
    {
        [XmlAttribute("nChannel")]
        public int nChannel { get; set; }
        [XmlAttribute("nTempr")] //温度值单位0.01℃
        public int nTempr { get; set; }
        [XmlAttribute("nHumidity")]
        public int nHumidity { get; set; } // 湿度值单位0.01%RH

    }

    #region   FluroData的子类
    //[XmlRoot("Nap")]
    public class CNap
    {

        [XmlAttribute("nID")]
        public int nID { get; set; }
        [XmlAttribute("nCurrentPos")]
        public int nCurrentPos { get; set; }
        [XmlElement("Cell")]
        public List<CCell> Cells { get; set; }
        public CNap()
        {
            Cells = new List<CCell>();
        }
    }
    public class CCell
    {

        [XmlAttribute("nPos")]
        public int nPos { get; set; }
        [XmlAttribute("strItemName")]
        public string strItemName { get; set; }
        [XmlAttribute("strEnzymeTime")]
        public string strEnzymeTime { get; set; } //加酶时间为时间字符串,如果为DateTime,转类时需单独处理会增加代码量
        [XmlElement("Result")]
        public CResult Result { get; set; }
    }


    public class CResult
    {
        [XmlAttribute("nCycleCount")]
        public int nCycleCount { get; set; }
        [XmlAttribute("dCt")]
        public double dCt { get; set; }
        [XmlAttribute("dConc")]
        public double dConc { get; set; }
        [XmlAttribute("nResult")]
        public int nResult { get; set; }

        [XmlElement("Channel")]
        public List<CChannel> Channels { get; set; }
        public CResult()
        {
            Channels = new List<CChannel>();
        }

        //[XmlElement("Channel1")]
        //public CChannel Channel1 { get; set; }
        //[XmlElement("Channel2")]
        //public CChannel Channel2 { get; set; }


    }
    public class CChannel
    {
        [XmlAttribute("nPos")]
        public int nPos { get; set; }
        [XmlAttribute("strTime")]
        public string strTime { get; set; }
        [XmlAttribute("strRaw")]
        public string strRaw { get; set; }
        [XmlAttribute("strValue")]
        public string strValue { get; set; }
    }
    #endregion
    #endregion
}
