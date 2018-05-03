using RDS.Models;
using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDS.ViewModels
{
    public class SetupViewModel : ViewModel
    {
        public AutoSAT RuntimeData => General.RuntimeData;

        private string message = $"请输入管理员口令";
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                this.RaisePropertyChanged(nameof(Message));
            }
        }

        private bool capsLockState;
        public bool CapsLockState
        {
            get { return capsLockState; }
            set
            {
                capsLockState = value;
                this.RaisePropertyChanged(nameof(CapsLockState));
            }
        }

        public bool IsUserMode { get; set; } = true;

        public bool IsExamineMode { get; set; } = true;

        public bool IsRunByWater { get; set; } = true;

        public bool IsDebug { get; set; } = true;

        public bool SaveButtonEnable { get; private set; }

        private int viewIndex;
        public int ViewIndex
        {
            get { return viewIndex; }
            set
            {
                viewIndex = value;
                this.RaisePropertyChanged(nameof(ViewIndex));
            }
        }

        public Language SelectedLanguage { get; set; }

        public string HeartingMinimizeTemperature { get; set; } = string.Empty;

        public string HeartingMaximizeTemperature { get; set; } = string.Empty;

        public string ReaderMinimizeTemperature { get; set; } = string.Empty;

        public string ReaderMaximizeTemperature { get; set; } = string.Empty;

        public RelayCommand Save { get; private set; }

        public RelayCommand ValidateAdministrators => new RelayCommand(() =>
        {
            this.RaisePropertyChanged(nameof(this.RuntimeData));

            this.OnViewChanged(new SetupViewChangedArgs(ViewChangedOption.ValidateAdministrators, Properties.Resources.Password));
        });

        public RelayCommand ClearPassword { get; private set; }

        public RelayCommand SelectionChanged { get; private set; }

        public RelayCommand ExitView { get; private set; }

        public RelayCommand EnterNext { get; private set; }

        public RelayCommand EnterPrevious { get; private set; }

        public RelayCommand GotFocus => new RelayCommand(() =>
        {
            this.GetCapslockState();
        });

        public enum ViewChangedOption
        {
            ValidateAdministrators = 0,
            ClearPassword = 1
        }

        public class SetupViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public SetupViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public SetupViewModel()
        {
            this.ClearPassword = new RelayCommand(() => this.OnViewChanged(new SetupViewChangedArgs(ViewChangedOption.ClearPassword, null)));

            this.Save = new RelayCommand(this.ExecuteSaveConfiguration);

            this.ExitView = new RelayCommand(() => { this.ViewIndex = 0; this.ClearPassword.Execute(null); });

            this.EnterNext = new RelayCommand(() => { this.ViewIndex = 2; });

            this.EnterPrevious = new RelayCommand(() => { this.ViewIndex = 1; });

            this.InitializeConfiguration();

            General.UpdateSelectedItemReagents();
        }

        private void InitializeConfiguration()
        {
            var r = General.Configuration;
            //this.SelectedLanguage = General.Configuration.Language.FirstOrDefault(o => o.IsUsed);
        }

        private void SaveLanguageConfiguration()
        {
            //foreach (var item in General.Configuration.Language)
            //{
            //    if (item.Equals(this.SelectedLanguage)) item.IsUsed = true;
            //    else item.IsUsed = false;
            //}
        }

        private void ExecuteSaveConfiguration()
        {
            this.SaveLanguageConfiguration();
            General.SaveConfiguration();
            this.ExitView.Execute(null);
            General.LoadLanguage();
        }

        public void ChangeMessage(bool isPassed)
        {
            Message = isPassed ? $"请输入管理员口令" : $"输入的口令不正确";
        }

        public void GetCapslockState()
        {
            this.CapsLockState = Console.CapsLock;
        }
    }
}
