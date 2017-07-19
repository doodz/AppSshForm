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
            return !(value as bool?);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
