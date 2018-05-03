using RDS.Models;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace RDS.ViewModels.Result
{
    public class HistroyViewModel1 : ViewModel
    {
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

        public ObservableCollection<History> HistroyInformations { get; set; }

        public List<History> Histroy { get; set; }

        public List<string> AgeItems { get; private set; }

        public List<string> SexItems { get; private set; }

        public List<string> UsedProjects { get; private set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int Age { get; set; }

        public string Barcode { get; set; }

        public string Id { get; set; }

        public string Class{get;set;}

        public string Begin { get; set; }

        public string End { get; set; }

        public string Birthday { get; set; }

        public RelayCommand Stretch { get; private set; }
        
        public RelayCommand ShowChart => new RelayCommand((o)=> 
        {
            this.OnViewChanged(new HistroyViewChangedArgs(ViewChangedOption.ShowChartView,o));
        });

        public RelayCommand ResetDateTime { get; private set; }

        public RelayCommand Export { get; private set; }

        public RelayCommand ChangePopupState { get; private set; }

        public HistroyViewModel1()
        {
            this.ChangePopupState = new RelayCommand((o) => this.PopupState = Convert.ToBoolean(o) ? Visibility.Visible : Visibility.Collapsed);
            this.Stretch = new RelayCommand(this.ExecuteStretch);
            //this.ShowChart = new RelayCommand(this.ExecuteShowChart);
            this.ResetDateTime = new RelayCommand(this.ExecuteResetDateTime);
            this.Export = new RelayCommand(()=> {; });
            this.InitializeAge();
            this.InitializeSex();
            this.InitializeProject();
            this.InitializeUsedreagents();
        }

        private void InitializeUsedreagents()
        {
          
        }

        private void InitializeAge()
        {
            this.AgeItems = new List<string>(150) { General.FindStringResource(Properties.Resources.HistroyView_SexItem_Both) };
            for (int i = 1; i <= 150; i++)
            {
                this.AgeItems.Add(i.ToString());
            }
        }

        private void InitializeProject()
        {
            
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

        private void ExecuteResetDateTime(object obj)
        {
            switch (Convert.ToInt32(obj))
            {
                case 0: { this.Birthday = string.Empty; break; }
                case 1: { this.Begin = string.Empty; break; }
                case 2: { this.End = string.Empty; break; }
                default: break;
            }
        }

        private void ExecuteShowChart(object obj)
        {
            
            this.InitializeUsedreagents();
            this.OnViewChanged(new HistroyViewChangedArgs(ViewChangedOption.ShowChartView, obj));
        }

        private void ExecuteStretch()
        {

         
            //this.HistroyInformations = new ObservableCollection<History>();
            //if (cells != null)
            //{
            //    foreach (var cell in cells)
            //    {
            //        History histroy = new History
            //        {
            //            Name = cell.PatientName ?? string.Empty,
            //            Sex = General.GetEnumDescription(cell.Sex),
            //            Barcode = cell.Barcode ?? string.Empty,
            //            Id = cell.SampleId ?? string.Empty,
            //            Item = cell.ItemName,
            //            Result = General.GetEnumDescription((ResultEnum)cell.Result.ResultValue),
            //            Type = cell.SampleType,
            //            DtValue = cell.Result.Ct,
            //            ExperimentDate = cell.ExpDate ?? string.Empty
            //        };
            //        var result = cell.Result;
            //        string[,] charts = new string[6, 2];
            //        charts[0, 0] = result.Channels[0].Value ?? string.Empty;
            //        charts[1, 0] = result.Channels[1].Value ?? string.Empty;
            //        charts[2, 0] = General.SDK.GetNegative(cell).Result?.Channels[0].Value ?? string.Empty;
            //        charts[3, 0] = General.SDK.GetNegative(cell).Result?.Channels[1].Value ?? string.Empty;
            //        charts[4, 0] = General.SDK.GetPositive(cell).Result?.Channels[0].Value ?? string.Empty;
            //        charts[5, 0] = General.SDK.GetPositive(cell).Result?.Channels[1].Value ?? string.Empty;

            //        charts[0, 1] = result.Channels[0].Time ?? string.Empty;
            //        charts[1, 1] = result.Channels[1].Time ?? string.Empty;
            //        charts[2, 1] = General.SDK.GetNegative(cell).Result?.Channels[0].Time ?? string.Empty;
            //        charts[3, 1] = General.SDK.GetNegative(cell).Result?.Channels[1].Time ?? string.Empty;
            //        charts[4, 1] = General.SDK.GetPositive(cell).Result?.Channels[0].Time ?? string.Empty;
            //        charts[5, 1] = General.SDK.GetPositive(cell).Result?.Channels[1].Time ?? string.Empty;
            //        histroy.Charts = charts;
            //        HistroyInformations.Add(histroy);
            //    }
            //}
            //this.RaisePropertyChanged(nameof(this.HistroyInformations));
            //this.PopupState = Visibility.Collapsed;

           
        }

        private void ExecuteExportInformation()
        {
            if(this.Histroy!=null)
            {
                var rows = new StringBuilder();
                var head = new StringBuilder();
                var descriptions = Models.History.GetDescriptions();
                for (int i = 0; i < descriptions.Count; i++) head.AppendFormat(Properties.Resources.StringFormat2, descriptions[i], Properties.Resources.Separator4);
                rows.AppendLine(head.ToString());

                foreach (var item in this.Histroy)
                {
                    var itemString = new StringBuilder();
                    var properties = item.GetPropetyValues();
                    for (int i = 0; i < properties.Count; i++) itemString.AppendFormat(Properties.Resources.StringFormat2, properties[i], Properties.Resources.Separator4);
                    rows.AppendLine(itemString.ToString());
                }

                var saveFile = new SaveFileDialog
                {
                    InitialDirectory = string.Format(Properties.Resources.StringFormat2, Environment.CurrentDirectory, Properties.Resources.ExportFilePath),
                    DefaultExt = string.Format(Properties.Resources.DefaultExt, Properties.Resources.Csv),
                    AddExtension = true,
                    Filter = string.Format(Properties.Resources.Filter, Properties.Resources.Csv),
                    OverwritePrompt = true,
                    CheckPathExists = true,
                    FileName = DateTime.Now.ToString(Properties.Resources.DateFormat)
                };

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (General.CheckFileIsUsed(saveFile.FileName))
                        {
                            //General.PopupWindow(PopupType.OneButton, General.FindStringResource(Properties.Resources.PopupWindow_Title_MessageBox), General.FindStringResource(Properties.Resources.Message18), new List<string> { General.FindStringResource(Properties.Resources.Button_Ok) });
                        }
                        else
                        {
                            //File.WriteAllText(saveFile.FileName, rows.ToString());
                            //General.PopupWindow(PopupType.OneButton, General.FindStringResource(Properties.Resources.PopupWindow_Title_MessageBox), General.FindStringResource(Properties.Resources.Message17), new List<string> { General.FindStringResource(Properties.Resources.Button_Ok) });
                        }
                    }
                    catch
                    {
                        //General.PopupWindow(PopupType.OneButton, General.FindStringResource(Properties.Resources.PopupWindow_Title_MessageBox), ex.Message, new List<string> { General.FindStringResource(Properties.Resources.Button_Ok) });
                    }
                }
               
            }
            else
            {
                General.PopupWindow("无法导出空信息，请确保列表装载有效数据。", new PopupMode[] { PopupMode.Ok });
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

