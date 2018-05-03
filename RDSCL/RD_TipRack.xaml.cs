using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RDSCL
{
    /// <summary>
    /// RD_TipRack.xaml 的交互逻辑
    /// </summary>
    public partial class RD_TipRack : UserControl
    {
        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_TipRack), new PropertyMetadata(null));

        public RD_TipRack()
        {
            InitializeComponent();
        }
    }
}
