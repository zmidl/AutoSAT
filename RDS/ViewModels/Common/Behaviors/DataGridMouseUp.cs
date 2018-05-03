using RDS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	public class DataGridMouseUp : Behavior<UIElement>
	{
		public object SelectedItems
		{
			get { return GetValue(SelectedItemsProperty); }
			set { SetValue(SelectedItemsProperty, value); }
		}
		public static readonly DependencyProperty SelectedItemsProperty =
			DependencyProperty.Register(nameof(SelectedItems), typeof(object), typeof(DataGridMouseUp), new PropertyMetadata(null));

		public object ViewModel
		{
			get { return GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
		   DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(DataGridMouseUp), new PropertyMetadata(null));

		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//((SampleViewModel)this.ViewModel).ManualSampling(this.SelectedItems);
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
