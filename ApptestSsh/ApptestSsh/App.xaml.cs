using System;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.MainPage;
using Autofac;
using Doods.StdLibSsh.Interfaces;
using Doods.StdRepository.Interfaces;
using Xamarin.Forms;

namespace ApptestSsh.Core
{
    public partial class App : Application
    {
        private static IContainer _container;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    throw new Exception("Please initialize the container first, through SetupContainer");

                return _container;
            }
        }

        public static void SetupContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            builder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            builder.RegisterType<SQLiteFactory>().As<ISQLiteFactory>().SingleInstance();
            _container = builder.Build();
        }

        protected override void OnStart()
        {
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
