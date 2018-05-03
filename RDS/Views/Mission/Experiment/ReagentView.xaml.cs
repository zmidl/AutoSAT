using RDS.ViewModels.Common;
using RDS.ViewModels.Mission.Experiment;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// ReagentView.xaml 的交互逻辑
    /// </summary>
    public partial class ReagentView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
        public ReagentViewModel ViewModel { get { return this.DataContext as ReagentViewModel; } }

        public ReagentView()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.ViewModel.AddHandler(ViewModel_ViewChanged);
        }

        private void ViewModel_ViewChanged(object sender, EventArgs e)
        {
            ((IExitView)this).ExitView();
        }

        
    }
}
