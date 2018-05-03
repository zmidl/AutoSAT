using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Mission.Experiment;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RDS.ViewModels.Common.Behaviors
{
    public class StripMultipSelect : MultipSelect
    {
        public override object ViewModel
        {
            get { return GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(StripMultipSelect), new PropertyMetadata(null));

        protected override HitTestResultCallback MouseDownHitTestResultCallback => new HitTestResultCallback(result => HitTestResultBehavior.Continue);

        protected override HitTestResultCallback MouseMoveHitTestResultCallback
        {
            get
            {
                return new HitTestResultCallback(result =>
                {
                    var viewModel = (StripViewModel)this.ViewModel;
                    var obj = result.VisualHit;
                    if (obj != null)
                    {
                        if (obj.GetType() == typeof(Path))
                        {
                            var strip = General.GetParentElement<RDSCL.RD_Strip>(obj);

                            if (strip != null && frame.Width >= 5)
                            {
                                (strip.DataSource as Slot).IsLoaded = isLoaded;
                            }
                        }
                    }
                    return HitTestResultBehavior.Continue;
                });
            }
        }

        protected override HitTestResultCallback MouseUpHitTestResultCallback
        {
            get
            {
                return new HitTestResultCallback(result =>
                {
                    var viewModel = (StripViewModel)this.ViewModel;
                    var obj = result.VisualHit;
                    if (obj != null)
                    {
                        if (obj.GetType() == typeof(Path))
                        {
                            var strip = General.GetParentElement<RDSCL.RD_Strip>(obj);
                            if (strip != null)
                            {
                                var slot = strip.DataSource as Slot;
                                slot.IsLoaded = !slot.IsLoaded;
                                viewModel.RaiseSelectedUsedCount();
                            }
                        }
                    }
                    return HitTestResultBehavior.Continue;
                });
            }
        }
    }
}
