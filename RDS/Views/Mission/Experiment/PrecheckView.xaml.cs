using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using RDS.ViewModels.Common;
using System.Collections.Generic;
using RDS.ViewModels.Mission.Experiment;

namespace RDS.Views.Mission.Experiment
{
    /// <summary>
    /// PrecheckView.xaml 的交互逻辑
    /// </summary>
    public partial class PrecheckView : UserControl, IExitView
    {
        private PrecheckViewModel viewModel = new PrecheckViewModel();
        Action IExitView.ExitView { get; set; }
        private object PreviousContent;

        public PrecheckView()
        {
            InitializeComponent();
            this.DataContext = this.viewModel;
            this.PreviousContent = this.Content;
            this.viewModel.ViewChanged += ViewModel_ViewChanged;
            //General.SDK.DoorStatusChanged += EmergencyStopped;
        }

        private void EmergencyStopped(int door, int status)
        {
            if (door == 2 && status == 0) General.PopupWindow($"仪器已紧急停止", new PopupMode[] { PopupMode.Ok });
            else
            {
                
            }
        }

        private void ViewModel_ViewChanged(object sender, EventArgs e)
        {
            var arg = (PrecheckViewModel.PrecheckViewChangedArgs)e;
            if (arg.Option == PrecheckViewModel.ViewChangedOption.Exit)
            {
                ((IExitView)this).ExitView();
            }
            else
            {
                //var waitWindow = General.PopupWindow("初始化运行界面", new PopupMode[] { PopupMode.Wait });
                new Task(() =>
                {
                    //Thread.Sleep(2000);
                    //General.SDK.DoorStatusChanged -= EmergencyStopped;
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        //waitWindow.Close();
                        this.Content = new Monitor.MonitorView();
                    }));
                }).Start();
            }
        }
    }
}
