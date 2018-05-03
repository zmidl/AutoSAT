using RDS.ViewModels.Mission.Experiment;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RDS.ViewModels.Common.Behaviors
{
    public class SampleMultipSelect : MultipSelect
    {
        public override object ViewModel
        {
            get { return GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(SampleMultipSelect), new PropertyMetadata(null));

        protected override HitTestResultCallback MouseDownHitTestResultCallback => new HitTestResultCallback(result =>
        {
            var viewModel = (SampleViewModel)this.ViewModel;
            var obj = result.VisualHit;
            if (obj != null)
            {
                if (obj.GetType() == typeof(Border))  this.IsHandled = true;
                else this.IsHandled = false;
                viewModel.AAA = $"{obj.GetType()}--{this.IsHandled}";
            }
            return HitTestResultBehavior.Continue;
        });

        protected override HitTestResultCallback MouseMoveHitTestResultCallback=> new HitTestResultCallback(result =>
        {
            var viewModel = (SampleViewModel)this.ViewModel;
            var obj = result.VisualHit;
            if (obj != null)
            {
                if (obj.GetType() == typeof(Border))
                {
                    var obj2 = General.GetParentElement<DataGridRow>(obj);
                    if (obj2 != null && frame.Width >= 5)  obj2.IsSelected = this.isLoaded;
                    
                }
            }
            return HitTestResultBehavior.Continue;
        });

        protected override HitTestResultCallback MouseUpHitTestResultCallback => new HitTestResultCallback(result =>
        {
            var viewModel = (SampleViewModel)this.ViewModel;
            var obj = result.VisualHit;
            if (obj != null)
            {
                if (obj.GetType() == typeof(Border))
                {
                    var obj2 = General.GetParentElement<DataGridRow>(obj);
                    if (obj2 != null) obj2.IsSelected = !obj2.IsSelected;
                    this.IsHandled = true;
                }
                else this.IsHandled = false;
            }
            return HitTestResultBehavior.Continue;
        });
    }
}
