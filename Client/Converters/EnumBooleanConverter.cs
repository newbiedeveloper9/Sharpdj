using System;
using System.Windows;
using System.Windows.Data;

namespace SharpDj.Converters
{
    public class EnumBooleanConverter : MarkuExtensionConverterBase<EnumBooleanConverter>
    {
        #region IValueConverter Members
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : Binding.DoNothing;
        }
        #endregion
    }
}
