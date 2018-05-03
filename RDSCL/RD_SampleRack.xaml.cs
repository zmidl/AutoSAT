using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RDSCL
{
	/// <summary>
	/// SampleTube.xaml 的交互逻辑
	/// </summary>
	public partial class RD_SampleRack : UserControl
	{
        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_SampleRack), new PropertyMetadata(null));

        public RD_SampleRack()
		{
			InitializeComponent();
		}
	}
}
