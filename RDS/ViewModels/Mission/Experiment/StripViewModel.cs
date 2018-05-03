using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using RDS.Apps;
using RDS.Models;
using System.Windows.Media;
using RDS.Models.RuntimeData.WorkPanel;

namespace RDS.ViewModels.Mission.Experiment
{
    public class StripViewModel : ViewModel
    {
        public List<Model> Models { get; set; } = General.RuntimeData.WorkPanel.Areas[2].Models;

        private SolidColorBrush test = General.ChartColor1;
        public SolidColorBrush Test
        {
            get { return test; }
            set
            {
                test = value;
                this.RaisePropertyChanged(nameof(Test));
            }
        }

        private bool[] isUsed = new bool[21];
        public object Values { get; set; }

        public int NeedingStripsCount => 0; //App.GlobalData.UsedNapCount;

        public int LoadedStripsCount { get; private set; }

        public int UnLoadStripsCount
        {
            get
            {
                var unSelectedUsedCount = this.NeedingStripsCount - this.LoadedStripsCount;
                if (unSelectedUsedCount < 0) unSelectedUsedCount = 0;
                return unSelectedUsedCount;
            }
        }

        public enum ViewChangedOption
        {
            ExitView = 0
        }

        public class StripViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public StripViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public Action<bool> SetStripResult { get; set; }

        public StripViewModel()
        {

        }

        public RelayCommand Reselect
        {
            get
            {
                return new RelayCommand(() =>
                {
                    for (int i = 0; i < this.isUsed.Length; i++)
                    {
                        this.isUsed[i] = false;
                        this.SetStripState(i, false);
                    }
                    this.RaiseSelectedUsedCount();
                });
            }
        }

        public RelayCommand Select
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    var stripIndex = Convert.ToInt32(o);
                    this.SetStripState(stripIndex);
                    this.UpdateSelectedUsedCount(stripIndex);
                });
            }
        }

        public RelayCommand Exit
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.UnLoadStripsCount != 0)
                    {
                        General.PopupWindow
                        (
                            "放置的六连排不够试验用量",
                            new PopupMode[] { PopupMode.Ok },
                            new Action[] { new Action(()=> { })}
                        );
                    }
                    else
                    {
                        this.SetStripResult(true);
                    
                        this.OnViewChanged(new StripViewChangedArgs(ViewChangedOption.ExitView, null));
                        this.SetStripResult = null;
                    }
                });
            }
        }

        public void SetStripState(int stripIndex, bool? isLoaded=null)
        {
            var modelIndex = stripIndex / 7;
            var slotIndex = stripIndex % 7;
            var strip = this.Models[modelIndex].Slots[slotIndex];
            if (isLoaded == null) strip.IsLoaded = !strip.IsLoaded;
            else strip.IsLoaded = isLoaded.Value;
            //strip.Content = (bool)strip.IsLoaded ? string.Empty : "可放置";
        }

        public void UpdateSelectedUsedCount(int stripIndex)
        {
            this.isUsed[stripIndex] = !this.isUsed[stripIndex];
            this.RaiseSelectedUsedCount();
        }

        public void UpdateSelectedUsedCount(int stripIndex, bool state)
        {
            this.isUsed[stripIndex] = state;
            this.RaiseSelectedUsedCount();
        }

        public void RaiseSelectedUsedCount()
        {
            this.LoadedStripsCount = this.isUsed.ToList().Where(o => o == true).Count();
            this.RaisePropertyChanged(nameof(this.LoadedStripsCount));
            this.RaisePropertyChanged(nameof(this.UnLoadStripsCount));
        }
    }
}

