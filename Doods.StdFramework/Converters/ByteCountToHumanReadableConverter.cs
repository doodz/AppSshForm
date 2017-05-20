using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Converters
{
    public class ByteCountToHumanReadableConverter : IValueConverter
    {
        public static ByteCountToHumanReadableConverter Default = new ByteCountToHumanReadableConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (long.TryParse(value.ToString(), out long r))
            {
                var param = true;
                if (parameter is bool)
                    param = (bool)parameter;

                return HumanReadableByteCount(r,param);
            }

            return 0L;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string HumanReadableByteCount(long bytes,bool si)
        {
            var unit = si ? 1000 : 1024;
            if (bytes < unit)
                return bytes + " B";
            var exp = (int)(Math.Log(bytes) / Math.Log(unit));
            var pre = (si ? "kMGTPE" : "KMGTPE").ElementAt(exp - 1)
                      + (si ? "" : "i");
            return string.Format("{0} {1}", bytes / Math.Pow(unit, exp), pre);
        }
    }
}
