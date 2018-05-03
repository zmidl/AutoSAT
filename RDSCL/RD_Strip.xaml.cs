using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RDSCL
{
    /// <summary>
    /// RD_Strip.xaml 的交互逻辑
    /// </summary>
    public partial class RD_Strip : UserControl
    {
        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_Strip), new PropertyMetadata(null));

        public RD_Strip()
        {
            InitializeComponent();
        }
    }

    public class BoolToVisibility : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Visibility.Hidden;
            if (Convert.ToBoolean(value)) result = Visibility.Visible;
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}