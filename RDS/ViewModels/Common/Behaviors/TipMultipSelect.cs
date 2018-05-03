using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Mission.Experiment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RDS.ViewModels.Common.Behaviors
{
    public class TipMultipSelect : MultipSelect
    {
        public override object ViewModel
        {
            get { return GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(MultipSelect), new PropertyMetadata(null));

        protected override HitTestResultCallback MouseDownHitTestResultCallback => new HitTestResultCallback(result => HitTestResultBehavior.Continue);

        protected override HitTestResultCallback MouseMoveHitTestResultCallback=> new HitTestResultCallback(result =>
        {
            var viewModel = (TipViewModel)this.ViewModel;
            var obj = result.VisualHit;
            if (obj is Ellipse tip)
            {
                var tipRack = General.GetParentElement<RDSCL.RD_TipRack>(obj);
                if (tipRack != null)
                {
                    if (Panel.GetZIndex(tipRack) == -1)
                    {
                        if (tip.DataContext is Cavity cavity)
                        {
                            if (cavity != null) cavity.IsLoaded = this.isLoaded;
                        }
                    }
                }
            }
            return HitTestResultBehavior.Continue;
        });

        protected override HitTestResultCallback MouseUpHitTestResultCallback=> new HitTestResultCallback(result =>
        {
            var viewModel = (TipViewModel)this.ViewModel;
            var obj = result.VisualHit;
            if (obj is Ellipse tip)
            {
                var tipRack = General.GetParentElement<RDSCL.RD_TipRack>(obj);
                if (tipRack != null)
                {
                    if (Panel.GetZIndex(tipRack) == -1)
                    {
                        if (tip.DataContext is Cavity cavity)
                        {
                            if (cavity != null) cavity.IsLoaded = !cavity.IsLoaded;
                        }
                    }
                }
            }
            return HitTestResultBehavior.Continue;
        });
    }
}
