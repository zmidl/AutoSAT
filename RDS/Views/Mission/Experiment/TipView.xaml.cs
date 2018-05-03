using RDS.ViewModels.Common;
using RDS.ViewModels.Mission.Experiment;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// TipView.xaml 的交互逻辑
    /// </summary>
    public partial class TipView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }
		public TipViewModel ViewModel { get { return this.DataContext as TipViewModel; } }
		public TipView()
		{
			InitializeComponent();
		}

        public TipView(object obj):this()
        {
            this.ViewModel.SelectTipRack.Execute(obj);
        }

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
            ((IExitView)this).ExitView();
        }
	}
}
