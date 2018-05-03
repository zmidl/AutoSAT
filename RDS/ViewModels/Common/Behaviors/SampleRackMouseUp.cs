
using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	public class SampleRackMouseUp : Behavior<UIElement>
	{
		public SampleRackIndex SampleRackIndex
		{
			get { return (SampleRackIndex)GetValue(SampleRackProperty); }
			set { SetValue(SampleRackProperty, value); }
		}
		public static readonly DependencyProperty SampleRackProperty =
			DependencyProperty.Register(nameof(SampleRackIndex), typeof(SampleRackIndex), typeof(SampleRackMouseUp), new PropertyMetadata(SampleRackIndex.RackA));

		public object ViewModel
		{
			get { return GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
		   DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(SampleRackMouseUp), new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
            if (sender is StackPanel)
			{
				var viewModel = (SampleViewModel)this.ViewModel;
				var currentSender = (StackPanel)sender;
				var name = currentSender.Name.Split(Properties.Resources.Separator5.ToCharArray()[0])[1];
				if (name == Properties.Resources.Auto) viewModel.ExecuteAutomaticSampling(this.SampleRackIndex);
				else if (name == Properties.Resources.Reset) viewModel.ExecuteResetSampleRack(this.SampleRackIndex);
				else viewModel.ExecuteManualSampling(this.SampleRackIndex);
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching(); this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
