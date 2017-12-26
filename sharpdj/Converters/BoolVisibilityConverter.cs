using System;
using System.Windows;

namespace SharpDj.Converters
{
    public class BoolVisibilityConverter : MarkuExtensionConverterBase<BoolVisibilityConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool && (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
