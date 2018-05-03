using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class TextBoxGotFocus : Behavior<UIElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.GotFocus += AssociatedObject_GotFocus;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
		}

		private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
		{
			//throw new NotImplementedException();
		}
	}
}
