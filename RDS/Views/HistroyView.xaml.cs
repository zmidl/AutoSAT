using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Data;
using RDS.ViewModels;

namespace RDS.Views
{
	public partial class HistroyView : UserControl
	{
        public HistroyViewModel ViewModel { get { return this.DataContext as HistroyViewModel; } }

        public HistroyView()
		{
			InitializeComponent();
			this.DataContext = new HistroyViewModel();
            this.ViewModel.AddHandler((s, e) =>
            {
                var args = (HistroyViewModel.HistroyViewChangedArgs)e;
                switch (args.Option)
                {
                    case HistroyViewModel.ViewChangedOption.ShowChartView:
                    {
                        General.ExitView(this, new ChartView(args.Value));
                        break;
                    }
                }
            });
		}
	}
}
