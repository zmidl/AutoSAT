using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using RDS.ViewModels;
using System.Windows.Media;
using System.Linq;
using System.Windows.Input;
using RDS.ViewModels.Mission.Experiment;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// SampleView.xaml 的交互逻辑
    /// </summary>
    public partial class SampleView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }

        public SampleViewModel ViewModel { get { return this.DataContext as SampleViewModel; } }

        public SampleView()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.ViewModel.AddHandler((sender, args) =>
            {
                var eventArgs = (SampleViewModel.SampleViewChangedArgs)args;
                switch (eventArgs.Option)
                {
                    case SampleViewModel.ViewChangedOption.ExitView:
                    {
                        ((IExitView)this).ExitView();
                        break;
                    }
                    default: break;
                }
            });
            //this.AdaptDataGrid(General.Configuration.ClientType);
        }

        private void AdaptDataGrid(ClientType type)
        {
            if(type== ClientType.QC)
            {
                this.ucDataGrid.Columns[1].Visibility = Visibility.Collapsed;
                this.ucDataGrid.Columns[4].Visibility = Visibility.Collapsed;
                this.ucDataGrid.Columns[5].Visibility = Visibility.Collapsed;
                this.ucDataGrid.Columns[6].Visibility = Visibility.Collapsed;
                this.ucDataGrid.Columns[9].Visibility = Visibility.Collapsed;
            }
        }
    }
}
