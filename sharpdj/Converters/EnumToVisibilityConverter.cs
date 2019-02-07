using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharpDj.Converters
{
    public class EnumToVisibilityConverter : MarkuExtensionConverterBase<EnumToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || !(value is Enum))
                return Visibility.Collapsed;

            var currentState = value.ToString();
            var stateStrings = parameter.ToString();
            var found = false;

            foreach (var state in stateStrings.Split(','))
            {
                found = (currentState == state.Trim());

                if (found)
                    break;
            }

            return found ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}