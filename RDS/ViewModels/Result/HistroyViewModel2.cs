using RDS.Models;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace RDS.ViewModels.Result
{
    public class HistroyViewModel2 : ViewModel
    {
        private readonly string csv = string.Format(Properties.Resources.DefaultExt, Properties.Resources.Csv);
        private readonly string rds = string.Format(Properties.Resources.DefaultExt, Properties.Resources.Rds);
        private readonly string rdsFilter = string.Format(Properties.Resources.Filter, Properties.Resources.Rds);
        private readonly string csvFilter = string.Format(Properties.Resources.Filter, Properties.Resources.Csv);
        private readonly string defaultExt;
        private readonly string filter;

        private bool isShowActualResult = true;

        private int pageIndex = 1;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                if (value < 1) value = 1;
                pageIndex = value;
                this.RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private Visibility popupState = Visibility.Hidden;
        public Visibility PopupState
        {
            get { return popupState; }
            set
            {
                popupState = value;
                this.RaisePropertyChanged(nameof(PopupState));
            }
        }

        private string resultUnit = string.Empty;
        public string ResultUnit
        {
            get { return resultUnit; }
            set
            {
                resultUnit = value;
                this.RaisePropertyChanged(nameof(ResultUnit));
            }
        }

        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();

        public List<History> Histories { get; set; } = new List<History>();

        public List<History> ActualResult { get; set; } = new List<History>();
       
        public HistroyViewModel2()
        {
            this.SubscribeEvent();
             this.defaultExt = $"{csv}{Properties.Resources.Separator9}{rds}";
             this.filter = $"{csvFilter}{Properties.Resources.Separator9}{rdsFilter}";
        }

        private void SubscribeEvent()
        {
           
            this.SearchViewModel.EventNotify += SearchViewModel_EventNotify;
        }

        private void SearchViewModel_EventNotify(object sender, object e)
        {
            //if (e != null)
            //{
            //    var cells = default(ObservableCollection<Cell>);
            //    cells = General.SDK.GetCells((Query)e);
            //    this.Histories = new List<History>();
            //    if (cells != null)
            //    {
            //        foreach (var cell in cells)
            //        {
            //            History histroy = new History
            //            {
            //                Name = cell.PatientName ?? string.Empty,
            //                Item = cell.ItemName,
            //                Sex = General.GetEnumDescription(cell.Sex),
            //                Barcode = cell.Barcode ?? string.Empty,
            //                Id = cell.SampleId ?? string.Empty
            //            };
            //            var result = cell.Result;
            //            if (result != null)
            //            {
            //                histroy.Result = result.ConcString ?? string.Empty;
            //                histroy.Assert = result.ConcResult ?? string.Empty;
            //                histroy.LogValue = result.LogConcResult ?? string.Empty;
            //            }
            //            histroy.Unit = cell.ItemName == Properties.Resources.HBV ? Properties.Resources.Copies : Properties.Resources.IU;
            //            histroy.ExperimentDate = cell.ExpDate ?? string.Empty;// Convert.ToDateTime(histroy.ExperimentDate).ToString(Properties.Resources.DateFormat1);
            //            Histories.Add(histroy);
            //        }
            //    }
            //    this.RaisePropertyChanged(nameof(this.Histories));
            //    this.isShowActualResult = false;
            //}
            //this.PopupState = Visibility.Collapsed;
        }

        private void UnregistEvent()
        {
            //SDK.Sdk.SdkCanDataReceivedEvent -= RefreshActualData;
        }

        //private void RefreshActualData(Experiment experiment)
        //{
        //    for (int i = 0; i < experiment.Naps.Count; i++)
        //    {
        //        var cells = experiment.Naps[i].Cells.Where(o => o.ItemName != string.Empty).ToArray();
        //        for (int j = 0; j < cells.Length; j++)
        //        {
        //            var history = new History
        //            {
        //                Name = cells[j].PatientName,
        //                Barcode = cells[j].Barcode,
        //                Id = cells[j].SampleId.ToString(),
        //                Sex = General.GetEnumDescription(cells[j].Sex),
        //                ExperimentDate = cells[j].ExpDate ?? string.Empty,//Convert.ToDateTime(cells[j].ExpDate).ToString(Properties.Resources.DateFormat1),
        //                Item = cells[j].ItemName,
        //                Result = cells[j].Result.ConcString,
        //                LogValue = cells[j].Result.LogConcResult,
        //                Assert = cells[j].Result.ConcResult,
        //                Unit = cells[j].ItemName == Properties.Resources.HBV ? Properties.Resources.Copies : Properties.Resources.IU
        //            };
        //            this.ActualResult.Add(history);
        //        }
        //    }
        //    if (this.isShowActualResult)
        //    {
        //        this.Histories = new List<History>();
        //        this.Histories.AddRange(this.ActualResult.ToArray());
        //        this.RaisePropertyChanged(nameof(this.Histories));
        //    }
        //    //throw new NotImplementedException();
        //}

        public enum ViewChangedOption
        {
            ShowChartView = 0
        }

        public class HistroyViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public HistroyViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public RelayCommand Popdown
        {
            get
            {
                return new RelayCommand(() => this.PopupState = Visibility.Collapsed);
            }
        }

        public RelayCommand ShowChart
        {
            get
            {
                return new RelayCommand((o) => this.OnViewChanged(new HistroyViewChangedArgs(ViewChangedOption.ShowChartView, o)));
            }
        }

        public RelayCommand Popup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.PopupState = Visibility.Visible;
                    this.SearchViewModel.InitializeDateTime();
                });
            }
        }

        public RelayCommand Import
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var xmlFilter = string.Format(Properties.Resources.Filter, Properties.Resources.Xml);
                    var filter = $"{xmlFilter}{Properties.Resources.Separator9}{rdsFilter}";
                    var openFileDialog = new OpenFileDialog
                    {
                        Title = "选择数据源文件",
                        Filter = this.filter,// "xml文件|*.xml",
                        FileName = string.Empty,
                        FilterIndex = 1,
                        Multiselect = false,
                        RestoreDirectory = true,
                        DefaultExt = Properties.Resources.Xml
                    };
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            this.Histories = new List<History>();
                            var resultTable = General.ReadExcel(openFileDialog.FileName, openFileDialog.SafeFileName.Split('.')[0]);
                            //var tables = XmlOperation.ReadXmlFile(openFileDialog.FileName);
                            //var cellTable = tables.Tables[Properties.Resources.Cell];
                            //var resultTable = tables.Tables[Properties.Resources.Result];

                            for (int i = 0; i < resultTable.Rows.Count; i++)
                            {
                                var cell = resultTable.Rows[i];
                                var result = resultTable.Rows[i];
                                var date = string.Empty;
                                var concentrationString = result[Properties.Resources.strConc].ToString();

                                var isDouble = double.TryParse(concentrationString, out double concentrationDouble);
                                if (isDouble)
                                {
                                    concentrationString = concentrationDouble < 1000 ? (Convert.ToInt32(concentrationDouble)).ToString() : concentrationDouble.ToString(Properties.Resources.E);
                                }

                                this.Histories.Add(new History
                                {
                                    Name = cell[Properties.Resources.strName].ToString(),
                                    Id = cell[Properties.Resources.strSampleID].ToString(),
                                    ExperimentDate = Convert.ToDateTime(result[Properties.Resources.strBeginReadTime]).ToString(Properties.Resources.DateFormat1),
                                    Item = cell[Properties.Resources.strItemName].ToString(),
                                    Result = concentrationString,
                                    LogValue = result[Properties.Resources.strLogConcResult].ToString(),
                                    Assert = result[Properties.Resources.strLogConcResult].ToString(),
                                    Unit = cell[Properties.Resources.strItemName].ToString() == Properties.Resources.HBV ? Properties.Resources.Copies : Properties.Resources.IU
                                });
                            }
                            this.RaisePropertyChanged(nameof(this.Histories));
                        }
                        catch (Exception message)
                        {
                            System.Windows.MessageBox.Show(message.Message);
                        }
                    }
                });
            }
        }

        public RelayCommand Export
        {
            get
            {
                return new RelayCommand(() =>
                {
                   
                    var saveFile = new SaveFileDialog
                    {
                        InitialDirectory = string.Format(Properties.Resources.StringFormat2, Environment.CurrentDirectory, Properties.Resources.ExportFilePath),
                        DefaultExt = defaultExt,
                        AddExtension = true,
                        Filter = filter,
                        OverwritePrompt = true,
                        CheckPathExists = true,
                        FileName = DateTime.Now.ToString(Properties.Resources.DateFormat)
                    };

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        var extension = Path.GetExtension(saveFile.FileName).Split(Properties.Resources.Separator6.ToCharArray()[0])[1];
                        if (extension.Equals(Properties.Resources.Csv) || extension.Equals(Properties.Resources.Rds))
                        {
                            try
                            {
                                File.WriteAllText(saveFile.FileName, this.WriteCsv().ToString(), Encoding.GetEncoding(Properties.Resources.GB2312));
                                General.PopupWindow(General.FindStringResource(Properties.Resources.Message17), new PopupMode[] { PopupMode.Ok });
                            }
                            catch (Exception ex)
                            {
                                General.PopupWindow(ex.Message, new PopupMode[] { PopupMode.Ok });
                            }
                        }
                        else if (extension.Equals(Properties.Resources.Pdf))
                        {

                        }
                        else if (extension.Equals(Properties.Resources.Jpg))
                        {

                        }
                        else
                        {

                        }
                    }
                });
            }
        }

        public RelayCommand ShowActualResult
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.isShowActualResult = true;
                    this.Histories = new List<History>();
                    this.Histories.AddRange(this.ActualResult.ToArray());
                    this.RaisePropertyChanged(nameof(this.Histories));
                });
            }
        }

        private string WriteCsv()
        {
            var result = new StringBuilder();
            var head = new StringBuilder();
            List<string> descriptions = new List<string> { "名称", "条码", "管号", "性别", "实验日期", "项目", "浓度", "Log", "结果", "单位" };

            for (int i = 0; i < descriptions.Count; i++) head.AppendFormat(Properties.Resources.StringFormat2, descriptions[i], Properties.Resources.Separator4);
            result.AppendLine(head.ToString());

            foreach (var item in this.Histories)
            {
                var itemString = new StringBuilder();
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Name, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Barcode, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Id, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Sex, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.ExperimentDate, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Item, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Result, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.LogValue, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Assert, Properties.Resources.Separator4);
                itemString.AppendFormat(Properties.Resources.StringFormat2, item.Unit, Properties.Resources.Separator4);
                result.AppendLine(itemString.ToString());
            }
            return result.ToString();
        }

        public static IList<Object[]> ReadDataFromCSV(string filePathName)
        {
            List<Object[]> ls = new List<Object[]>();
            StreamReader fileReader = new StreamReader(filePathName, Encoding.Default);
            string line = "";
            while (line != null)
            {
                line = fileReader.ReadLine();
                if (String.IsNullOrEmpty(line))
                    continue;
                String[] array = line.Split(';');
                ls.Add(array);
            }
            fileReader.Close();
            return ls;
        }
    }
}
