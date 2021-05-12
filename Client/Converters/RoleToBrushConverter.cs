using System;
using System.Globalization;
using System.Windows.Media;

namespace SharpDj.Converters
{
    class RoleToBrushConverter : MarkuExtensionConverterBase<RoleToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          /*  if (!(value is Rank))
                throw new ArgumentException("Value not of type Rank");

            switch ((Rank)value)
            {
                case Rank.Admin:
                    return new SolidColorBrush(new Color() { R = 20, G = 155, B = 10, A = byte.MaxValue });
                case Rank.Moderator:
                    return new SolidColorBrush(new Color() { R = 190, G = 170, B = 10, A = byte.MaxValue });
                case Rank.User:
                    return new SolidColorBrush(new Color() { R = 170, G = 165, B = 170, A = byte.MaxValue });
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }*/
          return new SolidColorBrush(new Color() { R = 170, G = 165, B = 170, A = byte.MaxValue });
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
