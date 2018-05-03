using RDS.Apps;
using RDS.ViewModels.Common;
using RDS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RDS.ViewModels.Mission.Experiment
{
    public class PrecheckViewModel : ViewModel
    {
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                this.RaisePropertyChanged(nameof(SelectedIndex));
            }
        }

        //public PopupWindow PopupWindow { get; private set; }

        private Visibility circlePogressState;
        public Visibility CirclePogressState
        {
            get { return circlePogressState; }
            set
            {
                circlePogressState = value;
                this.RaisePropertyChanged(nameof(CirclePogressState));
            }
        }

        private bool isSecondEnabled;
        public bool IsSecondEnabled
        {
            get { return isSecondEnabled; }
            set
            {
                isSecondEnabled = value;
                this.RaisePropertyChanged(nameof(IsSecondEnabled));
            }
        }

        private bool isThirdEnabled;
        public bool IsThirdEnabled
        {
            get { return isThirdEnabled; }
            set
            {
                isThirdEnabled = value;
                this.RaisePropertyChanged(nameof(IsThirdEnabled));
            }
        }

        private bool isFourthEnabled;
        public bool IsFourthEnabled
        {
            get { return isFourthEnabled; }
            set
            {
                isFourthEnabled = value;
                this.RaisePropertyChanged(nameof(IsFourthEnabled));
            }
        }

        private bool isFirstChecked;
        public bool IsFirstChecked
        {
            get { return isFirstChecked; }
            set
            {
                isFirstChecked = value;
                this.RaisePropertyChanged(nameof(IsFirstChecked));
            }
        }

        private bool isSecondChecked;
        public bool IsSecondChecked
        {
            get { return isSecondChecked; }
            set
            {
                isSecondChecked = value;
                this.RaisePropertyChanged(nameof(IsSecondChecked));
                if (value) this.Validate();
            }
        }

        private bool isThirdChecked;
        public bool IsThirdChecked
        {
            get { return isThirdChecked; }
            set
            {
                isThirdChecked = value;
                this.RaisePropertyChanged(nameof(IsThirdChecked));
                if (value) this.Validate();
            }
        }

        private bool isFourthChecked;
        public bool IsFourthChecked
        {
            get { return isFourthChecked; }
            set
            {
                isFourthChecked = value;
                this.RaisePropertyChanged(nameof(IsFourthChecked));
                if (value) this.Validate();
            }
        }

        public RelayCommand Test => new RelayCommand(()=>  this.OnViewChanged(new PrecheckViewChangedArgs(ViewChangedOption.Exit, null)));

        public PrecheckViewModel()
        {
            this.InitializeInstrument();
        }

        private void InitializeInstrument()
        {
            bool result = default(bool);
            this.CirclePogressState = Visibility.Visible;
            this.IsSecondEnabled = false;
            this.IsThirdEnabled = false;
            this.IsFourthEnabled = false;
            Task.Factory.StartNew(() =>
            {
                //System.Threading.Thread.Sleep(1000);
                result = true;
                //if (General.Configuration.IsDebug == false) result = General.SDK.Init();
                //else result = true;
            }).ContinueWith(task =>
            {
                App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {

                    this.CirclePogressState = Visibility.Hidden;
                    this.IsSecondEnabled = true;
                    this.IsThirdEnabled = true;
                    this.IsFourthEnabled = true;
                    this.IsFirstChecked = result;

                    var actions = new Action[3];
                    actions[0] = new Action(() => this.InitializeInstrument());
                    actions[1] = new Action(() => this.OnViewChanged(new PrecheckViewChangedArgs(ViewChangedOption.Exit, null)));
                    if (result == false)
                    {
                        General.PopupWindow(General.FindStringResource(Properties.Resources.PopupWindow_CheckInstrumentErrorMessage), new PopupMode[] { PopupMode.Ok, PopupMode.Cancel }, actions);
                    }
                }));
            });
        }

        private void Validate()
        {
            if (this.IsFirstChecked && this.IsSecondChecked && this.IsThirdChecked && this.IsFourthChecked)
            {
                //this.PopupWindow = General.PopupWindow("初始化", new PopupMode[1] { PopupMode.Wait });
                this.OnViewChanged(new PrecheckViewChangedArgs(ViewChangedOption.Enter, null));
                //new Task(() =>
                //{

                //    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.Content = new Monitor.MonitorView(); popupWindow.Close(); }));
                //}).Start();
            }
        }

        public enum ViewChangedOption
        {
            Exit = 0,
            Enter = 1
        }

        public class PrecheckViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public PrecheckViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }
    }
}
