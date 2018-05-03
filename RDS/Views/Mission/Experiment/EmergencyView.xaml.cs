using RDS.ViewModels;
using RDS.ViewModels.Common;
using RDS.ViewModels.Mission.Experiment;
using System;
using System.Windows.Controls;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// EmergencyView.xaml 的交互逻辑
    /// </summary>
    public partial class EmergencyView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }

		public EmergencyViewModel ViewModel { get { return this.DataContext as EmergencyViewModel; } }

		public EmergencyView()
		{
			InitializeComponent();
            this.ViewModel.AddHandler((s, args) =>
            {
                var eventArgs = (EmergencyViewModel.EmergencyViewChangedArgs)args;
                if (eventArgs.Option == EmergencyViewModel.ViewChangedOption.ExitView)
                {
                    ((IExitView)this).ExitView();
                }
            });
        }
    }
}
