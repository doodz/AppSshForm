using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.StdFramework.Converters
{
    public class TimeSpanToDateStringConverter : IValueConverter
    {

        public static TimeSpanToDateStringConverter Default = new TimeSpanToDateStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double)) return value;

            var timeSpan = TimeSpan.FromSeconds((double)value);

            if (parameter is string str)
            {
                return timeSpan.ToString(str);
            }

            return timeSpan.ToString("");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
