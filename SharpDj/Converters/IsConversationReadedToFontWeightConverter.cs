using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SharpDj.Models;

namespace SharpDj.Converters
{
    public class IsConversationReadedToFontWeightConverter : MarkuExtensionConverterBase<IsConversationReadedToFontWeightConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type bool");

            return (bool)value ? FontWeights.Regular : FontWeights.Bold;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
