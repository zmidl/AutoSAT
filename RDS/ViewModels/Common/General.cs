using RDS.Apps;
using RDS.Models.RuntimeData.Config;
using RDS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RDS.ViewModels.Common
{
    public static partial class General
    {
        public static void Minimized()
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        public static void ShutDown()
        {
            Application.Current.Shutdown();
        }

        public static void InitializeMainWindow(MainWindow mainWindow)
        {
            General.mainWindow = mainWindow;
        }

        public static void FindSetupView(SetupView setupView)
        {
            General.setupView = setupView;
        }

        public static string FindStringResource(string resourceKey)
        {
            return General.FindResource(resourceKey).ToString();
        }

        public static PathGeometry FindPathResource(string resourceKey)
        {
            return (General.FindResource(resourceKey) as PathGeometry);
        }

        public static object FindResource(string resourceKey)
        {
            return App.Current.FindResource(resourceKey);
        }

        public static PopupWindow PopupWindow(string message, PopupMode[] modes, Action[] actions = null)
        {
            var result = default(PopupWindow);
            App.Current.Dispatcher.Invoke(() =>
            {
                if (actions == null) actions = new Action[3];
                PopupWindow popupWindow = new PopupWindow();
                popupWindow.ViewModel.PopupWindow(message, modes, actions);
                popupWindow.Show();
                result = popupWindow;
            });
            return result;
        }

        public static string ReadConfiguration(string configurationKey)
        {
            //return ConfigurationManager.AppSettings[configurationKey].ToString();
            return string.Empty;
        }

        public static bool WriteConfiguration(string configurationKey, string value)
        {
            //var result = false;
            //try
            //{
            //    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //    configuration.AppSettings.Settings[configurationKey].Value = value;
            //    configuration.Save();
            //    result = true;
            //}
            //catch { }
            //return result;
            return false;
        }

        public static void ExitView(ContentControl currentContent, IExitView iExitView)
        {
            var oldContent = currentContent.Content;
            iExitView.ExitView = new Action(() => { currentContent.Content = oldContent; });
            currentContent.Content = iExitView;
        }

        public static DataTable GetLisInformation(string dateTime)
        {
            var result = default(DataTable);

            var lisFilesPath = string.Format
            (
                Properties.Resources.LisFilesPath,
                /*Directory.GetCurrentDirectory()*/
                Environment.CurrentDirectory,
                dateTime
            );

            if (File.Exists(lisFilesPath)) result = XmlOperation.ReadXmlFile(lisFilesPath).Tables[2];

            return result;
        }

        public static DataTable ReadExcel(string fileName, string sheetName)
        {
            var result = new DataTable();
            string excelConnectionString = string.Format(Properties.Resources.ExcelConnectionString, fileName);
            string excelCommandString = string.Format(Properties.Resources.ExcelCommandString, sheetName);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(excelCommandString, excelConnectionString)) adapter.Fill(result);
            }
            return result;
        }

        public static string GetEnumDescription(Enum enumObject)
        {
            Type type = enumObject.GetType();
            System.Reflection.MemberInfo[] memberInfos = type.GetMember(enumObject.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                if (memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attrs && attrs.Length > 0)
                {
                    return attrs[0].Description;
                }
            }
            return enumObject.ToString();
        }

        public static T GetParentElement<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            T result = default(T);
            var aaa = typeof(T).ToString();
            while (parent != null)
            {
                if (parent is T)
                {
                    result = (T)parent; break;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return result;
        }

        public static void StartProcess(string path, string fileName)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = string.Format(Properties.Resources.StringFormat3, Environment.CurrentDirectory, path, fileName),
                UseShellExecute = false,
                WorkingDirectory = string.Format(Properties.Resources.StringFormat2, Environment.CurrentDirectory, path)
            };

            processStartInfo.CreateNoWindow = true;

            Process.Start(processStartInfo);
        }

        private static void StopProcess(string processName)
        {
            Process.GetProcesses()?.FirstOrDefault(o => o.ProcessName == processName).Kill();
        }

        public static string XmlSerialize<T>(T entity)
        {
            var result = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(entity.GetType());
                serializer.Serialize(stringWriter, entity);
                result = stringWriter.ToString();
            }
            return result;
        }

        public static T XmlDeserialize<T>(string xmlString)
        {
            var result = default(T);
            using (StringReader stringReader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stringReader);
            }
            return result;
        }

        private static string JsonSerialize<T>(T entity)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(entity.GetType());
            string result = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                jsonSerializer.WriteObject(stream, entity);
                result = System.Text.Encoding.UTF8.GetString(stream.ToArray()).Trim();
            }
            return result;
        }

        private static T JsonDeserialize<T>(string _string)
        {
            T result = default(T);
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_string)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                result = (T)jsonSerializer.ReadObject(ms);
            }
            return result;
        }

        public static void ReadConfiguration()
        {
            General.Configuration = General.JsonDeserialize<Configuration>(File.ReadAllText(General.JsonFilePath, System.Text.Encoding.Default));
        }

        public static void SaveConfiguration()
        {
            File.WriteAllText(General.JsonFilePath, General.JsonSerialize(General.Configuration), System.Text.Encoding.Default);
        }

        public static void LoadLanguage()
        {
            //if (General.CurrentLanguageResource != null) App.Current.Resources.MergedDictionaries.Remove(General.CurrentLanguageResource);
            //var languageFilePath = string.Format(General.Configuration.LanguageFilePath, General.Configuration.Language.FirstOrDefault(o => o.IsUsed).FileName);
            //General.CurrentLanguageResource = Application.LoadComponent(new Uri(languageFilePath, UriKind.Relative)) as ResourceDictionary;
            //if (General.CurrentLanguageResource != null) App.Current.Resources.MergedDictionaries.Add(General.CurrentLanguageResource);
        }

        public static bool CheckFileIsUsed(string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None)) { return false; }
            }
            catch
            {
                return true;
            }
        }

        public static string Substring(string value, int subLength)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]", System.Text.RegularExpressions.RegexOptions.Compiled);
            var stringChar = value.ToCharArray();
            var stringBuilder = new System.Text.StringBuilder();
            int length = 0;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch(stringChar[i].ToString())) length += 2;
                else length += 1;

                if (length <= subLength) stringBuilder.Append(stringChar[i]);
                else { stringBuilder.Append("..."); break; }
            }
            return stringBuilder.ToString();
        }

        public static void UpdateSelectedItemReagents()
        {
            if (General.RuntimeData != null)
            {
                General.SelectedItemReagentNames = new List<string> { Properties.Resources.EmptyName, Properties.Resources.EmptyName, Properties.Resources.EmptyName, Properties.Resources.EmptyName };
                var cache = General.RuntimeData.SpecialUsage.Items.Where(o => o.Status == 1).Select(o => o.Memo).Take(4).ToList();
                for (int i = 0; i < cache.Count; i++) General.SelectedItemReagentNames[i] = cache[i];
            }
        }


        /// <summary>
        /// 获取当天实验次数
        /// </summary>
        /// <returns></returns>
        public static int GetExpNo()
        {
            int experimentNumber = 0;
            string value = ReadConfiguration(Properties.Resources.ExpNo);
            string date = value.Substring(0, 8);
            string number = value.Substring(9, value.Length - 9);
            //if (date == App.GlobalData.ExpDate) experimentNumber = Convert.ToInt32(number) + 1;
           // else experimentNumber = 1;
            value = string.Format
            (
                Properties.Resources.StringFormat3,
                DateTime.Now.ToString(Properties.Resources.DateFormat),
                Properties.Resources.Separator1,
                experimentNumber
            );
            WriteConfiguration(Properties.Resources.ExpNo, value);
            return experimentNumber;
        }
    }
}