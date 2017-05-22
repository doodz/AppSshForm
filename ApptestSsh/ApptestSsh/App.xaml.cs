using System;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.Login;
using ApptestSsh.Core.View.MainPage;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdLibSsh.Interfaces;
using Doods.StdRepository.Interfaces;
using Xamarin.Forms;

namespace ApptestSsh.Core
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage
            MainPage = new NavigationPage();
            NavigationService.Navigation = MainPage.Navigation;
           
        }


        public static void SetupContainer(AppSetup appSetup)
        {

            appSetup.ContainerBuilder.RegisterType<MainPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<LoginViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<SQLiteFactory>().As<ISQLiteFactory>().SingleInstance();
            var container = appSetup.Build();
        }
        //public static void SetupContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterType<MainPageViewModel>().AsSelf();
        //    builder.RegisterType<LoginViewModel>().AsSelf();
        //    builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
        //    builder.RegisterType<SshService>().As<ISshService>().SingleInstance();
        //    builder.RegisterType<SQLiteFactory>().As<ISQLiteFactory>().SingleInstance();
        //    _container = builder.Build();
        //}

        protected async override void OnStart()
        {
            await NavigationService.GoToHome();
            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
