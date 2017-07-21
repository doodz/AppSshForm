using System;
using System.Linq;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Helpers;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.RootPage.Android;
using ApptestSsh.Core.View.RootPage.Windows;
using ApptestSsh.Core.View.WelcomeStartPage;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using NLog;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using ILogger = Doods.StdFramework.Interfaces.ILogger;

namespace ApptestSsh.Core
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            FirstLogin();           
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
                    MainPage = new RootPageAndroid();
                    NavigationService.Navigation = MainPage.Navigation;
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

            MobileCenter.Start("ios={a1180a5d-28a0-4ea3-9eda-7b2aa8d4cba3};android={e3bb5908-01c3-4286-811f-b07537a6a632};uwp={4fb715f8-a0ed-46ce-8ac8-9785a6ca11e4}", typeof(Analytics), typeof(Crashes));


            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            //MessagingCenter.Subscribe<object, Exception>(this, Messages.ExceptionOccurred, OnAppExceptionOccurred);
            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

        private void FirstLogin()
        {
            MainPage = new WelcomeStartPage();
            NavigationService.Navigation = MainPage.Navigation;
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
