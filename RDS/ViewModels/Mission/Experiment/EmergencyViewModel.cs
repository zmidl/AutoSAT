using RDS.ViewModels.Common;
using System;

namespace RDS.ViewModels.Mission.Experiment
{
    public class EmergencyViewModel : ViewModel
    {
        public enum ViewChangedOption
        {
            ExitView = 0
        }

        public class EmergencyViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public EmergencyViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

       

        public RelayCommand Exit { get; private set; }

        public EmergencyViewModel()
        {
          
            this.Exit = new RelayCommand(() =>
            {
                this.OnViewChanged(new EmergencyViewChangedArgs(ViewChangedOption.ExitView, null));
            });
        }
    }
}
