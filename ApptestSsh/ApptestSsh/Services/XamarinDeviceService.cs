using System;
using System.Threading.Tasks;
using Doods.StdLibSsh.Interfaces;
using Xamarin.Forms;

namespace ApptestSsh.Core.Services
{
    public class XamarinDeviceService : IXamarinDeviceService
    {
        public Task Call(string tel)
        {
            try
            {
                Device.OpenUri(new Uri($"tel:{tel}"));
            }
            catch (Exception e)
            {
            }

            return Task.FromResult(0);
        }

        public Task Email(string adresse)
        {
            try
            {
                Device.OpenUri(new Uri($"mailto:{adresse}"));
            }
            catch (Exception e)
            {
            }

            return Task.FromResult(0);
        }

        public Task NavigateToAdresse(string adresse)
        {
            try
            {
                Device.OpenUri(new Uri($"google.navigation:q={adresse}"));
            }
            catch (Exception e)
            {
            }

            return Task.FromResult(0);
        }

        public Task NavigateToPosition(double latitude, double longitude)
        {
            try
            {
                Device.OpenUri(
                    new Uri(
                        $"google.navigation:q={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}"));
            }
            catch (Exception e)
            {
            }

            return Task.FromResult(0);
        }
    }
}
