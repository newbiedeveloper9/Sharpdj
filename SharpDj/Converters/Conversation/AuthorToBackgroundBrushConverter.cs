using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace SharpDj.Converters.Conversation
{
    public class AuthorToBackgroundBrushConverter : MarkuExtensionConverterBase<AuthorToBackgroundBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type Bool");

            var val = (bool)value;
            var color = Application.Current.Resources["PrimaryHueDarkBrush"];

            return val ? color :
                new SolidColorBrush(new Color() { R = 241, G = 241, B = 241, A = 230 });
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
