using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace SharpDj.Converters
{
    public abstract class MarkuExtensionConverterBase<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        private static T _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}
