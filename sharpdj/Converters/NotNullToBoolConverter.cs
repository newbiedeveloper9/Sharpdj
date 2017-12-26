using System;

namespace SharpDj.Converters
{
    public class NotNullToBoolConverter : MarkuExtensionConverterBase<NotNullToBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
