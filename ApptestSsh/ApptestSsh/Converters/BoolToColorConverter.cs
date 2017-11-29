using System;
using System.Globalization;
using Xamarin.Forms;

namespace ApptestSsh.Core.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public static readonly BoolToColorConverter Default = new BoolToColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (value is bool);

            if (val)
                val = (bool)value;

            return val ? Color.Green : Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
