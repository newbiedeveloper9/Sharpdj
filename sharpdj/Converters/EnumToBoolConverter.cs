using System;
using System.Windows;

namespace SharpDj.Converters
{
    public class EnumToBoolConverter : MarkuExtensionConverterBase<EnumToBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameter.ToString());

            return parameterValue.Equals(value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.Parse(targetType, parameter.ToString());
        }
    }
}
