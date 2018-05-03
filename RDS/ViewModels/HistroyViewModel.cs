using RdEntity;
using RDS.Models;
using RDS.ViewModels.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace RDS.ViewModels
{
    public class HistroyViewModel : ViewModel
    {
        private Visibility popupState;
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

        private Query queryTerm = new Query();
        public Query QueryTerm
        {
            get { return queryTerm; }
            set
            {
                queryTerm = value;
                this.RaisePropertyChanged(nameof(QueryTerm));
            }
        }

        public ObservableCollection<SelectedReagent> SelectedReagents { get; set; } = new ObservableCollection<SelectedReagent>();

        public ObservableCollection<HistroyInformation> HistroyInformations { get; set; }

        public List<string> AgeItems { get; private set; }

        public List<string> SexItems { get; private set; }

        public List<string> UsedProjects { get; private set; }

        public RelayCommand Stretch { get; private set; }

        public RelayCommand ShowChart { get; private set; }

        public RelayCommand ResetDateTime { get; private set; }

        public RelayCommand Export { get; private set; }

        public RelayCommand ControlPopup { get; private set; }

        public HistroyViewModel()
        {
            this.ControlPopup = new RelayCommand(this.ExecuteControlPopup);
            this.Stretch = new RelayCommand(/*this.ExecuteStretch*/this.ExecuteReadExcel);
            this.ShowChart = new RelayCommand(this.ExecuteShowChart);
            this.ResetDateTime = new RelayCommand(this.ExecuteResetDateTime);
            this.Export = new RelayCommand(this.ExecuteExportInformation);
            this.InitializeAge();
            this.InitializeSex();
            this.InitializeProject();
        }

        private void InitializeAge()
        {
            this.AgeItems = new List<string>(150);
            this.AgeItems.Add(General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both));
            for (int i = 1; i <= 150; i++)
            {
                this.AgeItems.Add(i.ToString());
            }
        }

        private void InitializeProject()
        {
            this.UsedProjects = General.Configuration.SampleType.ToList();
            this.UsedProjects.Insert(0, General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both));
        }

        private void InitializeSex()
        {
            this.SexItems = new List<string>(3)
            {
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both),
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Man),
                General.FindStringResource(Properties.Resources.HistroyView_SexItem_Woman)
            };
        }

        private void ExecuteControlPopup(object obj)
        {
            this.PopupState = Convert.ToInt32(obj) == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ExecuteResetDateTime(object obj)
        {
            switch (Convert.ToInt32(obj))
            {
                case 1: { this.QueryTerm.Begin = string.Empty; break; }
                case 2: { this.QueryTerm.End = string.Empty; break; }
                default: break;
            }
        }

        private void ExecuteShowChart(object obj)
        {
            this.OnViewChanged(new HistroyViewChangedArgs(ViewChangedOption.ShowChartView, obj));
        }

        private void ExecuteStretch()
        {
            var one = Properties.Resources.One;
            var zero = Properties.Resources.Zero;
            var selectedReagents = new bool[4];
            for (int i = 0; i < SelectedReagents.Count; i++) selectedReagents[i] = SelectedReagents[i].IsSelected;
            this.QueryTerm.Item = string.Join(string.Empty, selectedReagents.Select(o => o ? one : zero).ToArray());
            var cells = default(ObservableCollection<Cell>);
            cells = General.SDK.GetCells(this.QueryTerm);
            this.HistroyInformations = new ObservableCollection<HistroyInformation>();
            if (cells != null)
            {
                foreach (var cell in cells)
                {
                    HistroyInformation histroyInformation = new HistroyInformation();
                    histroyInformation.Age = cell.Age.ToString();
                    histroyInformation.Sex = General.GetEnumDescription(cell.Sex);
                    histroyInformation.Barcode = cell.Barcode;
                    histroyInformation.Name = cell.PatientName;
                    histroyInformation.Item = cell.ItemName;
                    //histroyInformation.Result = General.GetEnumDescription((ResultEnum)cell.Result.ResultValue);
                    histroyInformation.Type = cell.SampleType;
                    histroyInformation.Id = cell.SampleId;
                    histroyInformation.DtValue = cell.Result.Ct;
                    histroyInformation.Concentration = cell.Result.Conc;
                    histroyInformation.ExperimentDate = cell.ExpDate;
                    string[,] charts = new string[6, 2];
                    charts[0, 0] = cell.Result.Channels[0].Value ?? string.Empty;
                    charts[1, 0] = cell.Result.Channels[1].Value ?? string.Empty;
                    charts[2, 0] = General.SDK.GetNegative(cell).Result?.Channels[0].Value ?? string.Empty;
                    charts[3, 0] = General.SDK.GetNegative(cell).Result?.Channels[1].Value ?? string.Empty;
                    charts[4, 0] = General.SDK.GetPositive(cell).Result?.Channels[0].Value ?? string.Empty;
                    charts[5, 0] = General.SDK.GetPositive(cell).Result?.Channels[1].Value ?? string.Empty;

                    charts[0, 1] = cell.Result.Channels[0].Time ?? string.Empty;
                    charts[1, 1] = cell.Result.Channels[1].Time ?? string.Empty;
                    charts[2, 1] = General.SDK.GetNegative(cell).Result?.Channels[0].Time ?? string.Empty;
                    charts[3, 1] = General.SDK.GetNegative(cell).Result?.Channels[1].Time ?? string.Empty;
                    charts[4, 1] = General.SDK.GetPositive(cell).Result?.Channels[0].Time ?? string.Empty;
                    charts[5, 1] = General.SDK.GetPositive(cell).Result?.Channels[1].Time ?? string.Empty;
                    histroyInformation.Charts = charts;
                    HistroyInformations.Add(histroyInformation);
                }
            }
            this.RaisePropertyChanged(nameof(this.HistroyInformations));
        }

        private void ExecuteReadExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择数据源文件";
            openFileDialog.Filter = "xml文件|*.xml";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.HistroyInformations = new ObservableCollection<HistroyInformation>();
                    //var resultData = General.ReadExcel(openFileDialog.FileName, "Sheet1");
                    var tables = XmlOperation.ReadXmlFile(openFileDialog.FileName);
                    var cellTable = tables.Tables[Properties.Resources.Cell];
                    var resultTable = tables.Tables[Properties.Resources.Result];

                    for (int i = 0; i < resultTable.Rows.Count; i++)
                    {

                        var cell = cellTable.Rows[i];
                        var result = resultTable.Rows[i];
                        var date = string.Empty;

                        var concentrationString = result[Properties.Resources.fConc].ToString();
                        var concentrationDouble = 0d;
                        var isDouble = double.TryParse(concentrationString, out concentrationDouble);
                        if (isDouble)
                        {
                            concentrationString = concentrationDouble < 1000 ? (Convert.ToInt32(concentrationDouble)).ToString() : concentrationDouble.ToString(Properties.Resources.E);
                        }

                        this.HistroyInformations.Add(new HistroyInformation
                        {
                            Name = cell[Properties.Resources.strName].ToString(),
                            Id = cell[Properties.Resources.strSampleID].ToString(),
                            ExperimentDate = Convert.ToDateTime(result[Properties.Resources.strBeginReadTime]).ToString(Properties.Resources.DateFormat1),
                            Item = cell[Properties.Resources.strItemName].ToString(),
                            Result = concentrationString,
                            LogValue = result[Properties.Resources.strLog10Value].ToString(),
                            Assert = result[Properties.Resources.strIUperml].ToString(),
                            Unit = cell[6].ToString() == Properties.Resources.HBV ? Properties.Resources.Copies : Properties.Resources.IU
                        });
                    }
                    this.RaisePropertyChanged(nameof(this.HistroyInformations));
                }
                catch (Exception message)
                {
                    System.Windows.MessageBox.Show(message.Message);
                }
            }
        }

        private void ExecuteExportInformation()
        {
            var rows = new StringBuilder();
            var head = new StringBuilder();
            var descriptions = HistroyInformation.GetDescriptions();
            for (int i = 0; i < descriptions.Count; i++) head.AppendFormat(Properties.Resources.StringFormat2, descriptions[i], Properties.Resources.Separator4);
            rows.AppendLine(head.ToString());

            foreach (var item in this.HistroyInformations)
            {
                var itemString = new StringBuilder();
                var properties = item.GetPropetyValues();
                for (int i = 0; i < properties.Count; i++) itemString.AppendFormat(Properties.Resources.StringFormat2, properties[i], Properties.Resources.Separator4);
                rows.AppendLine(itemString.ToString());
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = string.Format(Properties.Resources.StringFormat2, Environment.CurrentDirectory, Properties.Resources.ExportFilePath);
            saveFile.DefaultExt = string.Format(Properties.Resources.DefaultExt, Properties.Resources.ExtensionName_Csv);
            saveFile.AddExtension = true;
            saveFile.Filter = string.Format(Properties.Resources.Filter, Properties.Resources.ExtensionName_Csv);
            saveFile.OverwritePrompt = true;
            saveFile.CheckPathExists = true;
            saveFile.FileName = DateTime.Now.ToString(Properties.Resources.DateFormat);

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (General.CheckFileIsUsed(saveFile.FileName))
                    {
                        General.PopupWindow(PopupMode.OneButton, PopupType.Message, General.FindStringResource(Properties.Resources.Message18));
                    }
                    else
                    {
                        File.WriteAllText(saveFile.FileName, rows.ToString());
                        General.PopupWindow(PopupMode.OneButton, PopupType.Message, General.FindStringResource(Properties.Resources.Message17));
                    }
                }
                catch (Exception ex)
                {
                    General.PopupWindow(PopupMode.OneButton, PopupType.Message, ex.Message);
                }
            }
        }

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
    }
}
