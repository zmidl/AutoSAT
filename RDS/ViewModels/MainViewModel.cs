using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RDS.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private bool isTask;
        public bool IsTask
        {
            get { return isTask; }
            set
            {
                if (isTask == false)
                {
                    isTask = value;
                    this.RaisePropertyChanged(nameof(IsTask));
                    this.isHistroy = false;
                    this.isHelp = false;
                    this.RaisePropertyChanged(nameof(this.IsHistroy));
                    this.RaisePropertyChanged(nameof(this.IsHelp));
                    this.ShowTaskView();
                }
            }
        }

        private bool isHistroy;
        public bool IsHistroy
        {
            get { return isHistroy; }
            set
            {
                if (isHistroy == false)
                {
                    isHistroy = value;
                    this.RaisePropertyChanged(nameof(IsHistroy));
                    this.isTask = false;
                    this.isHelp = false;
                    this.RaisePropertyChanged(nameof(this.IsTask));
                    this.RaisePropertyChanged(nameof(this.IsHelp));
                    this.ShowHistroyView();
                }
            }
        }

        private bool isHelp;
        public bool IsHelp
        {
            get { return isHelp; }
            set
            {
                if (isHelp == false)
                {
                    isHelp = value;
                    this.RaisePropertyChanged(nameof(IsHelp));
                    this.isTask = false;
                    this.isHistroy = false;
                    this.RaisePropertyChanged(nameof(this.IsTask));
                    this.RaisePropertyChanged(nameof(this.IsHistroy));
                    this.ShowHelpView();
                }
            }
        }

        public PopupWindowViewModel PopupWindowViewModel { get; set; } = new PopupWindowViewModel();

        public RelayCommand ExitApp { get; private set; }

        public RelayCommand ShowAdministratorsLogin { get; private set; }

        public RelayCommand ShowInformation { get; private set; }

        public RelayCommand Minimized { get; private set; }

        public enum ViewChangedOption
        {
            TaskView = 0,
            HistroyView = 1,
            HelpView = 2,
            SetupView = 3,
            ExitApp = 4
        }

        public class MainViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public MainViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }
        public MainViewModel()
        {
            //General.IsFluorReaderWorking = this.RunExeFromZheng();
            //var sql=new SQLiteHelper() ;
            //    var data = sql.GetResult("select * from Cells");
            this.ShowInformation = new RelayCommand(() => General.PopupWindow("AutoSAT", new PopupMode[] { PopupMode.Ok }));
            this.ShowAdministratorsLogin = new RelayCommand(this.ExecuteShowSetupView);

            this.Minimized = new RelayCommand(General.Minimized);

            this.ExitApp = new RelayCommand(this.ExecuteExitApp);

            this.IsTask = true;
        }

        private bool RunExeFromZheng()
        {
            var result = false;
            //if  ( RdCore.Reader.CShareDll.IsActive(RdCore.Reader.CRdsModule.strReaderServiceName) == RdCore.Reader.ErrCode.EC_OK)
            //{
            //    return true;
            //}

            try
            {

                General.StartProcess(Properties.Resources.RdsReaderWorkingDirectory, Properties.Resources.RdsReaderFileName);
                result = true;
            }
            catch
            {
                General.PopupWindow(General.FindStringResource(Properties.Resources.PopupWindow_Title_Error), new PopupMode[] { PopupMode.Ok });
            }
            return result;
        }

        private void ShowTaskView()
        {
            this.OnViewChanged(new MainViewChangedArgs(ViewChangedOption.TaskView, null));
        }

        private void ShowHistroyView()
        {
            this.OnViewChanged(new MainViewChangedArgs(ViewChangedOption.HistroyView, null));
        }

        private void ShowHelpView()
        {
            this.OnViewChanged(new MainViewChangedArgs(ViewChangedOption.HelpView, null));
        }

        private void ExecuteShowSetupView()
        {
            this.IsTask = false;
            this.IsHistroy = false;
            this.IsHelp = false;
            this.OnViewChanged(new MainViewChangedArgs(ViewChangedOption.SetupView, null));
        }

        private void ExecuteExitApp()
        {
            this.OnViewChanged(new MainViewChangedArgs(ViewChangedOption.ExitApp, null));
        }
    }
}
