using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpDj.Converters.Conversation
{
    class AuthorToFontForegroundConverter : MarkuExtensionConverterBase<AuthorToFontForegroundConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type Bool");

            var colorOwnMessage = Application.Current.Resources["MaterialDesignDarkForeground"];
            var color = Application.Current.Resources["MaterialDesignLightForeground"];

            return (bool)value ? colorOwnMessage : color;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
