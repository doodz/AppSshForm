using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.StdFramework.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Default = new InverseBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
