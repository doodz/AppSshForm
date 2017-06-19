using System;
using System.Linq;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Helpers;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.RootPage.Windows;
using ApptestSsh.Core.View.WelcomeStartPage;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using NLog;
using Xamarin.Forms;
using ILogger = Doods.StdFramework.Interfaces.ILogger;

namespace ApptestSsh.Core
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            FirstLogin();
            var res =Settings.Current.LastHostId;
            GetMainPage();
        }


        private async Task GetMainPage()
        {
            var repo = AppContainer.Container.Resolve<IRepository>();
            var list = await repo.GetAllAsync<Host>();
            if (!list.Any())
            {
                var logger = AppContainer.Container.Resolve<ILogger>();
                logger.Info("App start : No host in repository. Start first login.");
                //FirstLogin();
                return;
            }

            var ssh = AppContainer.Container.Resolve<ISshService>();
            Host host = null;
            if (Settings.Current.LastHostId > 0)
                host = list.First(l =>l.Id == Settings.Current.LastHostId);
            else
                host = list.First();
            ssh.Host = host;
            ssh.Initialise();
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    NavigationService.GoToHome();
                    break;
                case Device.iOS:
                    NavigationService.GoToHome();
                    break;
                case Device.UWP:
                case Device.WinPhone:
                    MainPage = new RootPageWindows();
                    NavigationService.Navigation = MainPage.Navigation;
                    //await NavigationService.GoToRootPageWindows(); 
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected async override void OnStart()
        {
            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            //MessagingCenter.Subscribe<object, Exception>(this, Messages.ExceptionOccurred, OnAppExceptionOccurred);
            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

        private void FirstLogin()
        {
            MainPage = new WelcomeStartPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            //Settings.Current.IsConnected = CrossConnectivity.Current.IsConnected;
            //CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }
    }
}
