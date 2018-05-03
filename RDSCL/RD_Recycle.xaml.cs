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

namespace RDSCL
{
	/// <summary>
	/// RD_Recycle.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Recycle : UserControl
	{
		public bool IsMoving
		{
			get { return (bool)GetValue(isMovingProperty); }
			set { SetValue(isMovingProperty, value); }
		}
		public static readonly DependencyProperty isMovingProperty =
			DependencyProperty.Register(nameof(IsMoving), typeof(bool), typeof(RD_Recycle), new PropertyMetadata(false));

		public RD_Recycle()
		{
			InitializeComponent();
		}
	}
}
