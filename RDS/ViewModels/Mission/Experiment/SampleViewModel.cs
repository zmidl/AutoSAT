using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RDS.ViewModels.Mission.Experiment
{
    public class SampleViewModel : ViewModel
    {

        private string aaa;
        public string AAA
        {
            get { return aaa; }
            set
            {
                aaa = value;
                this.RaisePropertyChanged(nameof(AAA));
            }
        }

        //public List<SampleRack> SampleRacks { get; set; }
        public List<Model> SampleRacks2 { get; set; }

        //public SampleRack CurrentSampleRack { get; set; }
        public Model CurrentSampleRack2 { get; set; }

        private uint sampleIncrement;
        private Window AutoSamplingDialog;
        private List<Patient> selectedPatientInfos;
        private DataTable lisInformation;

        public string[] SexOptions { get; private set; }
        public List<string> ClassOptions { get; private set; }

      
        public List<string> ExperimentNameOptions { get; set; }
      
        public string EditingText { get; private set; }

        private int inputtingPageIndex = 0;
        public int InputtingPageIndex
        {
            get { return inputtingPageIndex; }
            set
            {
                inputtingPageIndex = value;
                this.RaisePropertyChanged(nameof(InputtingPageIndex));
            }
        }

        public Visibility PopupVisibility { get; private set; } = Visibility.Hidden;

        public List<SelectedReagent> UsedItems { get; set; }

        public enum ViewChangedOption
        {
            ExitView = 0,
        }

        public class SampleViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public SampleViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public SampleViewModel()
        {
            this.SampleRacks2 = General.RuntimeData.WorkPanel.Areas[0].Models;
            this.InitializeSampleId();
            this.InitializeOptions();
            this.InitializeUsedReagents();
            this.InitializeLisInformation();
            this.SubscribeEvent();
        }

        private void InitializeSampleId()
        {
            //var date = General.Configuration.SampleMaximumId.Substring(0, 8);
            //var number = Convert.ToUInt32(General.Configuration.SampleMaximumId.Substring(7, 3));
            //if (DateTime.Now.ToString(Properties.Resources.DateFormat) == date) this.sampleIncrement = number + 1;
        }

        private void InitializeOptions()
        {
            //this.SexOptions = new string[] { Properties.Resources.NoSex, Properties.Resources.Male, Properties.Resources.Female };
            //this.ClassOptions = General.Configuration.Classes;
    
            //this.ExperimentNameOptions = General.Configuration.ExperimentNames;
        }

        private void SubscribeEvent()
        {
            //General.SDK.SdkMessage += SDK_SdkMessage;
        }

        private void UnsubscribeEvent()
        {
            //General.SDK.SdkMessage -= SDK_SdkMessage;
        }

        //private void SDK_SdkMessage(object sender, SDK.SdkMessage.State state)
        //{
        //    switch (state)
        //    {
        //        case SDK.SdkMessage.State.RequestExtraction:
        //        {
        //            this.AutoSamplingDialog = General.PopupWindow("请抽出样本载架", new PopupMode[] { PopupMode.Ok }, new Action[] { new Action(General.SDK.Abort) });
        //            break;
        //        }

        //        case SDK.SdkMessage.State.FinishExtraction:
        //        {
        //            this.AutoSamplingDialog.Close();
        //            break;
        //        }
        //        case SDK.SdkMessage.State.RequestInsert:
        //        {
        //            this.AutoSamplingDialog = General.PopupWindow("请插入样本载架", new PopupMode[] { PopupMode.Retry, PopupMode.Abort }, new Action[] { new Action(General.SDK.Retry), new Action(General.SDK.Abort) });
        //            ((Views.PopupWindow)this.AutoSamplingDialog).ViewModel.EnableRetryButton = false;
        //            break;
        //        }
        //        case SDK.SdkMessage.State.Failed:
        //        {
        //            ((Views.PopupWindow)this.AutoSamplingDialog).ViewModel.EnableRetryButton = true;
        //            break;
        //        }
        //        case SDK.SdkMessage.State.Retry:
        //        {
        //            ((Views.PopupWindow)this.AutoSamplingDialog).ViewModel.EnableRetryButton = false;
        //            break;
        //        }
        //        case SDK.SdkMessage.State.FinishInsert:
        //        {
        //            this.AutoSamplingDialog.Close();
        //            break;
        //        }
        //        default: break;
        //    }
        //}

        private void Sampling(int sampleRackIndex, string[] barCodes)
        {

        }

        //private void AssignLisToSample(string barcode, Sample sample)
        //{
        //    for (int i = 0; i < this.lisInformation.Rows.Count; i++)
        //    {
        //        if (this.lisInformation.Rows[i][0].ToString() == barcode)
        //        {
        //            sample.Age = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Age].ToString();
        //            sample.Barcode = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Barcode].ToString();
        //            sample.Birthday = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Birthday].ToString();
        //            sample.Date = this.lisInformation.Rows[i][Properties.Resources.LisInfo_DateTime].ToString();
        //            sample.Name = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Name].ToString();
        //            sample.Item = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Item].ToString();
        //            sample.Id = this.lisInformation.Rows[i][Properties.Resources.LisInfo_SampleID].ToString();
        //            sample.Sex = this.lisInformation.Rows[i][Properties.Resources.LisInfo_Sex].ToString();
        //            sample.Class = this.lisInformation.Rows[i][Properties.Resources.LisInfo_SampleType].ToString();
        //            sample.IsEmergency = false;
        //            break;
        //        }
        //    }
        //}



        private void InitializeUsedReagents()
        {
            this.UsedItems = new List<SelectedReagent>();
            //for (int i = 0; i < General.UsedReagents.Count; i++) this.UsedItems.Add(new SelectedReagent(false, General.UsedReagents[i].Name));
        }

        private void RefreshCurrentSampleRack(int index)
        {
            if (index == -1) this.CurrentSampleRack2 = null;
            else this.CurrentSampleRack2 = this.SampleRacks2[index];
            this.RaisePropertyChanged(nameof(this.CurrentSampleRack2));
        }

        private void SaveAllSampleInformationToGlobalData()
        {
            var aaa = General.RuntimeData.WorkPanel.Areas[0].Models[0].Slots[0].Tube.Cavities[0].WorkLiquide.Patient;
        }

        private void SaveSampleId()
        {
            //General.Configuration.SampleMaximumId = string.Format
            //(
            //    Properties.Resources.StringFormat2,
            //    DateTime.Now.ToString(Properties.Resources.DateFormat),
            //    this.sampleIncrement.ToString(Properties.Resources.ThreebleZero)
            //);
            //General.SaveConfiguration();
        }

        public void InitializeLisInformation()
        {
            try
            {
                string dateTime = string.Empty;

#if DEBUG
                dateTime = "20170524";
#else
                dateTime = DateTime.Now.ToString(Properties.Resources.DateTimeLong);
#endif
                this.lisInformation = General.GetLisInformation(dateTime);
            }
            catch
            {
                General.PopupWindow
                (
                    General.FindStringResource(Properties.Resources.PopupWindow_Message1), new PopupMode[] { PopupMode.Ok }
                );
            }
        }

        public RelayCommand SaveAndExit
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    var dataGrid = o as DataGrid;
                    dataGrid.CommitEdit();
                    dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                    this.SaveAllSampleInformationToGlobalData();
                    this.SaveSampleId();
                    this.UnsubscribeEvent();
                    this.OnViewChanged(new SampleViewChangedArgs(ViewChangedOption.ExitView, null));
                });
            }
        }
        public RelayCommand Exit => new RelayCommand(() =>
        {
            this.OnViewChanged(new SampleViewChangedArgs(ViewChangedOption.ExitView, null)); ;
        });

        public RelayCommand ResetDate
        {
            get
            {
                return new RelayCommand(() => {; });
            }
        }

        public RelayCommand ResetSampleRack
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    var sampleRack = this.SampleRacks2[Convert.ToInt32(o)];
                    sampleRack.CanManualSampling = true;
                    sampleRack.Clear();
                    sampleRack.IsLoaded = false;
                    this.RefreshCurrentSampleRack(-1);
                });
            }
        }

        public RelayCommand CancelSavingReagents
        {
            get
            {
                return new RelayCommand(() => { this.PopupVisibility = Visibility.Hidden; this.RaisePropertyChanged(nameof(PopupVisibility)); });
            }
        }

        public RelayCommand PressDelete
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    //var selectedItems = ((System.Collections.IList)o).Cast<Sample>().ToList();
                    //for (int i = 0; i < selectedItems.Count; i++) selectedItems[i].Clear();
                });
            }
        }

        public RelayCommand AutomaticSampling => new RelayCommand((o) =>
        {
            try
            {
                var sampleRackIndex = Convert.ToInt32(o);
                //this.Sampling(sampleRackIndex, General.SDK.ReadBarcode(sampleRackIndex).Values.ToArray());
                //this.SampleRacks[sampleRackIndex].CanManualSampling = false;
            }
            catch { }
        });

        public RelayCommand ManualSampling
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    var sampleRackIndex = Convert.ToInt32(o);
                    if (this.SampleRacks2[sampleRackIndex].CanManualSampling)
                    {
                        this.RefreshCurrentSampleRack(sampleRackIndex);
                        this.SampleRacks2[sampleRackIndex].IsLoaded = true;
                        this.SampleRacks2[sampleRackIndex].CanManualSampling = false;
                    }
                });
            }
        }
        public RelayCommand SelecteSampleRack
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    var rackIndex = Convert.ToInt32(o);
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == rackIndex) this.SampleRacks2[i].SelectionFrameStates = Visibility.Visible;
                        else this.SampleRacks2[i].SelectionFrameStates = Visibility.Hidden;
                    }
                    if (this.SampleRacks2[rackIndex].IsLoaded) this.RefreshCurrentSampleRack(rackIndex);
                    else this.RefreshCurrentSampleRack(-1);
                });
            }
        }

        public RelayCommand EditId
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (this.CurrentSampleRack2 != null)
                    {
                        //var selectedItems = ((System.Collections.IList)o).Cast<PatientInfo>().ToList();
                        //var dateFormat = DateTime.Now.ToString(Properties.Resources.DateFormat);
                        //var targetItems = new List<PatientInfo>();
                        //if (selectedItems == null || selectedItems.Count == 0) targetItems = this.CurrentSampleRack2.Slots[0].Tube.Cavities[0].PatientInfo;
                        //else targetItems = selectedItems;
                        //for (int i = 0; i < targetItems.Count; i++)
                        //{
                        //    if (this.sampleIncrement++ >= uint.MaxValue) this.sampleIncrement = uint.MinValue;
                        //    targetItems[i].Id = string.Format(Properties.Resources.StringFormat2, dateFormat, this.sampleIncrement.ToString(Properties.Resources.ThreebleZero));

                        //    if (General.Configuration.ExperimentType.Equals(ExperimentType.TwoStep)) targetItems[i].Item = General.Configuration.UsedItemName;
                        //}
                    }
                });
            }
        }

        public RelayCommand EditEmergency
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    //var selectedItems = ((System.Collections.IList)o).Cast<Sample>().ToList();
                    //if (this.CurrentSampleRack2 != null)
                    //{
                    //    if (selectedItems.Count == 0) selectedItems = this.CurrentSampleRack2.Samples;
                    //    var targetValue = false;
                    //    if (selectedItems.Where(obj => obj.IsEmergency == true).Count() == 0) targetValue = true;
                    //    for (int i = 0; i < selectedItems.Count; i++) selectedItems[i].IsEmergency = targetValue;
                    //}
                });
            }
        }

        public RelayCommand ShowItemEditbox
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    this.InputtingPageIndex = 0;
                    //if (General.Configuration.ExperimentType.Equals(ExperimentType.Qualitative )) this.ShowEditbos(this.InputtingPageIndex, o);
                    //else
                    //{
                    //    if (this.CurrentSampleRack2 != null)
                    //    {
                    //        this.selectedPatientInfos = this.GetSelectedSamples(o).ToList();
                    //        this.SaveEditedText.Execute(null);
                    //    }
                    //}
                });
            }
        }

        public RelayCommand ShowConcentrationEditbox { get { return new RelayCommand((o) => this.ShowEditbos(1, o)); } }

        public RelayCommand ShowGroupEditbox { get { return new RelayCommand((o) => this.ShowEditbos(2, o)); } }

        public RelayCommand ShowTypeSeriesEditbox { get { return new RelayCommand((o) => this.ShowEditbos(3, o)); } }

        public RelayCommand ShowTypeName { get { return new RelayCommand((o) => this.ShowEditbos(4, o)); } }

        public RelayCommand ShowDateEditbox { get { return new RelayCommand((o) => this.ShowEditbos(5, o)); } }

        public RelayCommand ShowSexEditbox { get { return new RelayCommand((o) => this.ShowEditbos(6, o)); } }

        public RelayCommand ShowClassEditbox { get { return new RelayCommand((o) => this.ShowEditbos(7, o)); } }

        public RelayCommand ShowAgeEditbox { get { return new RelayCommand((o) => this.ShowEditbos(8, o)); } }

        public RelayCommand ShowBarcodeEditbox { get { return new RelayCommand((o) => this.ShowEditbos(9, o)); } }

        public RelayCommand ShowNameEditbox { get { return new RelayCommand((o) => this.ShowEditbos(10, o)); } }

        public RelayCommand SaveEditedText
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var focusedElement = System.Windows.Input.Keyboard.FocusedElement as FrameworkElement;
                    if (focusedElement is TextBox) focusedElement.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                    var itemName = string.Join(Properties.Resources.Separator4, this.UsedItems.Where(o => o.IsSelected == true).Select(o => o.Name).ToArray());
                    for (int i = 0; i < this.selectedPatientInfos.Count; i++)
                    {
                        switch (this.InputtingPageIndex)
                        {
                            case 0:
                            {
                                //if (General.Configuration.ExperimentType == ExperimentType.Qualitative) this.selectedPatientInfos[i].Item = itemName;
                                //else this.selectedPatientInfos[i].Item = General.Configuration.UsedItemName;
                                break;
                            }
                            //case 1: { this.selectedPatientInfos[i].Concentration = this.GetConcentration(this.EditingText); break; }
                            //case 2: { this.selectedPatientInfos[i].Group = this.EditingText; break; }
                            //case 3: { this.selectedPatientInfos[i].TypeSeriesName = this.EditingText; break; }
                            //case 4: { this.selectedPatientInfos[i].TypeName = this.EditingText; break; }
                            case 5: { this.selectedPatientInfos[i].DateTime = this.EditingText; break; }
                            case 6: { this.selectedPatientInfos[i].Sex = this.EditingText; break; }
                            case 7: { this.selectedPatientInfos[i].Type = this.EditingText; break; }
                            case 8: { this.selectedPatientInfos[i].Age = this.EditingText; break; }
                            case 9: { this.selectedPatientInfos[i].Barcode = this.EditingText; break; }
                            case 10: { this.selectedPatientInfos[i].Name = this.EditingText; break; }
                            default: break;
                        }
                    }
                    this.PopupVisibility = Visibility.Hidden;
                    this.RaisePropertyChanged(nameof(PopupVisibility));
                });
            }
        }



        private void ShowEditbos(int index, object o)
        {
            this.InputtingPageIndex = index;
            if (this.CurrentSampleRack2 != null)
            {
                this.selectedPatientInfos = this.GetSelectedSamples(o).ToList();
                if (this.selectedPatientInfos.Count == 1)
                {
                    switch (index)
                    {
                        case 0:
                        {
                            var selectedItemNames = this.selectedPatientInfos[0].Item.Split(Properties.Resources.Separator4.ToCharArray()[0]);
                            this.ResetUsedItems();
                            for (int i = 0; i < selectedItemNames.Length; i++)
                            {
                                var item = this.UsedItems.FirstOrDefault(_o => _o.Name == selectedItemNames[i]);
                                if (item != null) item.IsSelected = true;
                            }
                            break;
                        }
                        //case 1: { this.EditingText = this.selectedPatientInfos[0].Concentration.ToString(Properties.Resources.E); break; }
                        //case 2: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Group) ? null : this.selectedPatientInfos[0].Group; break; }
                        // case 3: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].TypeSeriesName) ? null : this.selectedPatientInfos[0].TypeSeriesName; break; }
                        //case 4:
                        //    {
                        //        var name = this.selectedPatientInfos[0].TypeSeriesName ?? string.Empty;
                        //        this.TypeOptions = this.TypeSeriesOptions.FirstOrDefault(_o => _o.Name == name)?.Types;
                        //        this.RaisePropertyChanged(nameof(this.TypeOptions));
                        //        this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].TypeName) ? null : this.selectedPatientInfos[0].TypeName;
                        //        break;
                        //    }
                        case 5: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].DateTime) ? DateTime.Now.ToString(Properties.Resources.DateFormat) : this.selectedPatientInfos[0].DateTime; break; }
                        case 6: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Sex) ? null : this.selectedPatientInfos[0].Sex; break; }
                        case 7: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Type) ? null : this.selectedPatientInfos[0].Type; break; }
                        case 8: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Age) ? null : this.selectedPatientInfos[0].Age; break; }
                        case 9: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Barcode) ? null : this.selectedPatientInfos[0].Barcode; break; }
                        case 10: { this.EditingText = string.IsNullOrEmpty(this.selectedPatientInfos[0].Name) ? null : this.selectedPatientInfos[0].Name; break; }
                    }
                }
                else
                {
               
                    this.EditingText = null;
                }
                this.RaisePropertyChanged(nameof(this.EditingText));
                this.PopupVisibility = Visibility.Visible;
                this.RaisePropertyChanged(nameof(PopupVisibility));
            }
        }

        private long GetConcentration(string editingText)
        {
            var result = default(long);
            try { result = long.Parse(editingText, NumberStyles.Float); }
            catch { }
            return result;
        }

        private IEnumerable<Patient> GetSelectedSamples(object obj)
        {
            var result = ((System.Collections.IList)obj).Cast<Slot>();
            if (result.ToList().Count == 0) result = this.CurrentSampleRack2.Slots;
            foreach (var item in result)
            {
                yield return item.Tube.Cavities[0].WorkLiquide.Patient;
            }
        }

        private void ResetUsedItems()
        {
            for (int i = 0; i < this.UsedItems.Count; i++)
            {
                this.UsedItems[i].IsSelected = false;
            }
        }
    }
}
