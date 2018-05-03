using RDS.ViewModels;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RDS.Views
{
	/// <summary>
	/// ChartView.xaml 的交互逻辑
	/// </summary>
	public partial class ChartView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }
		public ChartViewModel ViewModel { get; }
		public ChartView(object obj)
		{
			InitializeComponent();
			this.ViewModel = new ChartViewModel(obj);
			this.DataContext = this.ViewModel;
            this.ViewModel.AddHandler((s, e) =>
            {
                var args = (ChartViewModel.ChartViewChangedArgs)e;
                switch (args.Option)
                {
                    case ChartViewModel.ViewChangedOption.ExitView:
                    {
                        ((IExitView)this).ExitView();
                        break;
                    }
                }
            });
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			this.ViewModel.IsChannel1Visible = true;
		}
	}
}
