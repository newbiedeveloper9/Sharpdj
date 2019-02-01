using System;
using System.Globalization;
using System.Windows.Data;

namespace SharpDj.Converters
{
    public class StringToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is string str ? str.ToUpper() : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
             null;
    }
}