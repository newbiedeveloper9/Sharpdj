using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace SharpDj.Converters
{
    public class NameToContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var userControl = Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + "." + value, null, null);
            return Activator.CreateInstance(userControl);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}