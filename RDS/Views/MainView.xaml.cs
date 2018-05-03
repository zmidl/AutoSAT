using RDS.ViewModels;
using RDS.ViewModels.Common;
using RDS.Views.Mission;
using RDS.Views.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;

namespace RDS.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        private SetupView setupView = new SetupView();

        private TaskView taskView = new TaskView();

        private HelpView helpView = new HelpView();

        private ResultView resultView = new ResultView();

        private object PreviousContent;

        public MainViewModel ViewModel { get { return this.DataContext as MainViewModel; } }

        public MainView()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();

            this.ContentControl_CurrentContent.Content = this.taskView;

            this.PreviousContent = this.ContentControl_CurrentContent.Content;

            this.ViewModel.AddHandler(ListenViewChanged);

          
        }

        private void Workbench_ReportMessageEvent(int type, string message, List<string> buttonContents, Action[] actions)
        {
            var modes = new PopupMode[buttonContents.Count];
            for (int i = 0; i < buttonContents.Count; i++)
            {
                switch (i)
                {
                    case 0:
                    {
                        modes[i] = PopupMode.Ok;
                        break;
                    }
                    case 1:
                    {
                        modes[i] = PopupMode.Retry;
                        break;
                    }
                    case 2:
                    {
                        modes[i] = PopupMode.Cancel;
                        break;
                    }
                    default: break;
                }
            }
            General.PopupWindow(message, modes, actions);
        }

        private void ListenViewChanged(object sender, EventArgs e)
        {
            switch (((MainViewModel.MainViewChangedArgs)e).Option)
            {
                case MainViewModel.ViewChangedOption.TaskView: { this.ContentControl_CurrentContent.Content = this.taskView; break; }
                case MainViewModel.ViewChangedOption.HistroyView:
                {
                    this.ContentControl_CurrentContent.Content = this.resultView;
                    break;
                }
                case MainViewModel.ViewChangedOption.HelpView: { this.ContentControl_CurrentContent.Content = this.helpView; break; }
                case MainViewModel.ViewChangedOption.SetupView:
                {
                    this.setupView.ViewModel.ViewIndex = 0;
                    this.setupView.ViewModel.ClearPassword.Execute(null);
                    this.ContentControl_CurrentContent.Content = this.setupView;
                    break;
                }
                case MainViewModel.ViewChangedOption.ExitApp:
                {
                    General.PopupWindow
                    (
                        General.FindStringResource(Properties.Resources.PopupWindow_Message4),
                        new PopupMode[] { PopupMode.Ok, PopupMode.Cancel },
                        new Action[2]
                        {
                            new Action(() => Application.Current.Shutdown()),//new Action(() =>  Environment.Exit(0)),
                            null
                        }
                    );
                    break;
                }
                default: break;
            }
        }
    }
}
