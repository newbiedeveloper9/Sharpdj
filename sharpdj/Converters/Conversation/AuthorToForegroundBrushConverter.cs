using System;
using System.Globalization;
using System.Windows;

namespace SharpDj.Converters.Conversation
{
    public class AuthorToForegroundBrushConverter : MarkuExtensionConverterBase<AuthorToForegroundBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type Bool");

            var colorOwnMessage = Application.Current.Resources["PrimaryHueDarkForegroundBrush"];
            var color = Application.Current.Resources["PrimaryHueLightForegroundBrush"];

            return (bool)value ? colorOwnMessage : color;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
