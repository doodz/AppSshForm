using System;
using System.Windows.Input;
using ApptestSsh.Core.Services;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.WelcomeStartPage
{
    public class WelcomeStartPageViewModel : BaseViewModel
    {
        private string _welcomMessage = "Welcome to SshPi";

        public ICommand GoToRootPageCmd { get; }

        public string WelcomMessage
        {
            get => _welcomMessage;
            set => SetProperty(ref _welcomMessage, value);
        }

        public WelcomeStartPageViewModel(ILogger logger) : base(logger)
        {

            GoToRootPageCmd = new Command(GoToRootPage);

        }

        private void GoToRootPage(object obj)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:

                    NavigationService.GoToRootPageAndroid();
                    break;
                case Device.iOS:
                    NavigationService.GoToHome();
                    break;
                case Device.UWP:
                case Device.WinPhone:

                    NavigationService.GoToRootPageWindows();
                    //await NavigationService.GoToRootPageWindows(); 
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}