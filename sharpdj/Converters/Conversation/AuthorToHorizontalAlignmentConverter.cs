using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpDj.Converters.Conversation
{
    public class AuthorToHorizontalAlignmentConverter : MarkuExtensionConverterBase<AuthorToHorizontalAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value not of type Bool");

            return (bool)value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
