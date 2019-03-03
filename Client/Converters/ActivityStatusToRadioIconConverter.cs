using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace SharpDj.Converters
{
    class ActivityStatusToRadioIconConverter : MarkuExtensionConverterBase<ActivityStatusToRadioIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type Boolean");

            return (bool)value ? PackIconKind.RadioButtonChecked : PackIconKind.RadioButtonUnchecked;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
