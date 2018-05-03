using RDS.ViewModels;
using RDS.ViewModels.Common;
using RDS.ViewModels.Mission.Experiment;
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

namespace RDS.Views.Monitor
{
    /// <summary>
    /// StripView.xaml 的交互逻辑
    /// </summary>
    public partial class StripView : UserControl, IExitView
    {
        
        Action IExitView.ExitView { get; set; }
        public StripViewModel ViewModel { get { return this.DataContext as StripViewModel; } }
        public StripView()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.ViewModel.AddHandler(ListenViewChanged);
        }

        private void ListenViewChanged(object sender, EventArgs e)
        {
            var args = (StripViewModel.StripViewChangedArgs)e;
            if (args.Option == StripViewModel.ViewChangedOption.ExitView) ((IExitView)this).ExitView();
        }

        private void RD_CupRack_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var aaa = sender;
        }

        //private void Canvas_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.ViewModel.InitializeStripView(this.Values);
        //}
    }
}
