using Omv.Rpc.StdClient.Datas;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Omv.Rpc.StdClient.Conterters
{
    public class SystemInfoDataToValueConverter : IValueConverter
    {
        public static readonly SystemInfoDataToValueConverter Default = new SystemInfoDataToValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SystemInfoData sysInfo)
            {
                switch (sysInfo.Type)
                {
                    case "string":
                        return sysInfo.Value;
                    case "progress":
                        // var val = (JObject)sysInfo.Value;


                        //return val.ToString();
                        return sysInfo.Value;
                }

            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
