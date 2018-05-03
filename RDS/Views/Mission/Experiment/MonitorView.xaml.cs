using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using RDS.ViewModels.Mission.Experiment;
using static RDS.ViewModels.Mission.Experiment.MonitorViewModel;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// MonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorView : UserControl
    {
        public MonitorViewModel ViewModel { get { return this.DataContext as MonitorViewModel; } }

        public MonitorView()
        {
            InitializeComponent();
            this.DataContext = new MonitorViewModel();
            this.ViewModel.AddHandler(ViewModel_ViewChanged);
            this.ViewModel.SetSamplingResult(false);
            this.ViewModel.SetStripResult(true);
        }

        private void ViewModel_ViewChanged(object sender, EventArgs e)
        {
            var args = (MonitorViewChangedArgs)e;
            switch (args.Option)
            {
                case ViewChangedOption.ShowSampleView:
                {
                    General.ExitView(this, new SampleView());
                    break;
                }
                case ViewChangedOption.ShowReagentView:
                {
                    General.ExitView(this, new ReagentView());
                    break;
                }
                case ViewChangedOption.ShowStripView:
                {
                    var stripView = new StripView();
                    stripView.ViewModel.SetStripResult = new Action<bool>(this.ViewModel.SetStripResult) ;
                    General.ExitView(this, stripView);
                    break;
                }
                case ViewChangedOption.ShowEmergencyView:
                {
                    General.ExitView(this,new EmergencyView());
                    break;
                }
                case ViewChangedOption.ShowTipView:
                {
                    General.ExitView(this,new TipView(args.Value));
                    break;
                }
                case ViewChangedOption.TaskStop:
                {
                    General.ExitView(this, (new MaintenanceView()));
                    break;
                }
                case ViewChangedOption.NotifySamplingResult:
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}
