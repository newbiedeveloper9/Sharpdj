using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SharpDj.Converters
{
    public class BoolToStatusConverter : MarkuExtensionConverterBase<BoolToStatusConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var imagesResources = new ResourceDictionary
            {
                Source = new Uri("/GxDirectGuiLib;component/ImagesResources.xaml", UriKind.RelativeOrAbsolute)
            };

            if (value is bool && (bool)value)
                return imagesResources["Ok-16x16.png"] as BitmapImage;

            return imagesResources["Close-2-16x16.png"] as BitmapImage;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
