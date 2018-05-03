using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RDS.ViewModels
{
    public class MaintenanceViewModel : ViewModel
    {
        public Visibility PreviouseButtonState{get { return this.WizardIndex == 0 ? Visibility.Hidden : Visibility.Visible; }}
        public Visibility NextButtonState { get { return this.WizardIndex == 6 ? Visibility.Hidden : Visibility.Visible; } }

        private readonly int WizardSize = 6;

        private int wizardIndex;
        public int WizardIndex
        {
            get { return wizardIndex; }
            set
            {
                if (value < 0) value = 0;
                else if (value > this.WizardSize) value = this.WizardSize;
                wizardIndex = value;
                this.RaisePropertyChanged(nameof(WizardIndex));
                this.RaisePropertyChanged(nameof(this.PreviouseButtonState));
                this.RaisePropertyChanged(nameof(this.NextButtonState));
            }
        }

        public RelayCommand TurnNextView { get; private set; }
        public RelayCommand TurnPreviousView { get; private set; }

        public MaintenanceViewModel()
        {
            this.WizardIndex = 0;
            this.TurnNextView = new RelayCommand(this.ExecuteTurnNextView);
            this.TurnPreviousView = new RelayCommand(this.ExecuteTurnPreviousView);
        }

        public enum ViewChangedOption
        {
            ExitMaintenanceView = 0,
            EnterFinalView = 1
        }

        public class MaintenanceViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public MaintenanceViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        private void ExecuteTurnNextView()
        {
            this.WizardIndex++;
            //if (this.WizardIndex++ == this.WizardSize) this.OnViewChanged(new MaintenanceViewChangedArgs(ViewChangedOption.EnterFinalView, null));
        }

        private void ExecuteTurnPreviousView()
        {
            if (this.WizardIndex-- == 0) this.OnViewChanged(new MaintenanceViewChangedArgs(ViewChangedOption.ExitMaintenanceView, null));
        }
    }
}
