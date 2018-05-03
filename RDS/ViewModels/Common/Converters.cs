
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RDS.ViewModels.Common
{
    public class SampleSelectionStateToSolidColorBrush : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputtedValue = (bool)value;
            var resutColor = default(SolidColorBrush);
            if (inputtedValue) resutColor = new SolidColorBrush(Colors.Black);
            else resutColor = new SolidColorBrush(Colors.White);
            return resutColor;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputtedValue = (SolidColorBrush)value;
            var result = true;
            if (inputtedValue.Color == Colors.White) result = false;
            return result;
        }
    }

    public class IsTaskStartedToCanEdit : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = !(bool)value;
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (bool)value;
            return !result;
        }
    }

    public class CanManualSamplingToSolidColorBrush : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = default(SolidColorBrush);
            var input = (bool)value;
            if (input) result = General.FindResource(Properties.Resources.BlueColor) as SolidColorBrush;
            else result = General.FindResource(Properties.Resources.WathetColor) as SolidColorBrush;
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null; //throw new NotImplementedException();
        }
    }

    public class CanAutoSamplingToSolidColorBrush : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = default(SolidColorBrush);
            var input = (bool)value;
            if (input) result = General.FindResource(Properties.Resources.WathetColor) as SolidColorBrush;
            else result = General.FindResource(Properties.Resources.BlueColor) as SolidColorBrush;
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null; //throw new NotImplementedException();
        }
    }

    public class DateTimeToString_yyyyMMdd : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Convert.ToInt32(parameter) == -1 && value != null && string.IsNullOrEmpty(value.ToString()) == false)
            {
                object result = null;
                try
                {
                    result = DateTime.ParseExact(value.ToString(), Properties.Resources.DateFormat, CultureInfo.CurrentCulture);
                }
                catch
                {
                    result = string.Empty;
                }
                return result;
            }
            return string.Empty;
            //return Convert.ToDateTime(value.ToString(),new DateTimeFormatInfo { ShortDatePattern=Properties.Resources.DateFormat});
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null) result = ((DateTime)value).ToString(Properties.Resources.DateFormat);
            return result;
        }
    }

    public class SexStringToSexEnum : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            return "未知";

        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
         
            return null;
        }
    }

    public class IntToAgeString : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "不限";
            if ((int)value >= 0) result = value.ToString();
            return result;
            //throw new NotImplementedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = -1;
            if (value != null && value.ToString() != "不限") result = int.Parse(value.ToString());
            return result;
        }
    }

    public class UnLimitedToString : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value == null) result = "不限";
            else result = value.ToString();
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = "不限";
            var result = value.ToString();
            if (result == "不限") result = string.Empty;
            return result;
        }
    }

    public class StringToSubstring : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return General.Substring(value.ToString(), Convert.ToInt32(parameter));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class IntToBool : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert.ToInt32(value) == 1 ? true : false;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert.ToBoolean(value) ? 1 : 0;
        }
    }
}


