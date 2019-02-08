using SharpDj.Models;
using System;
using System.Globalization;
using System.Windows.Media;

namespace SharpDj.Converters
{
    public class ActivityStatusToColorConverter : MarkuExtensionConverterBase<ActivityStatusToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RoomModel.Activity))
                throw new ArgumentException("Value not of type Activity");

            var val = (RoomModel.Activity)value;
            switch (val)
            {
                case RoomModel.Activity.Active:
                    return new SolidColorBrush(new Color() { R = 0, G = 255, B = 50, A = byte.MaxValue });
                case RoomModel.Activity.Sleep:
                    return new SolidColorBrush(new Color() { R = 255, G = 170, B = 0, A = byte.MaxValue });
                case RoomModel.Activity.InActive:
                    return new SolidColorBrush(new Color() { R = 126, G = 126, B = 126, A = byte.MaxValue });
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
