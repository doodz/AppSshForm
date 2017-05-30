using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.HomeTabbedPage;
using ApptestSsh.Core.View.HostManagerPage;
using ApptestSsh.Core.View.LoginPage;
using ApptestSsh.Core.View.MainPage;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdRepository.Base;
using Doods.StdRepository.Interfaces;
using Database = ApptestSsh.Core.DataBase.Database;

namespace ApptestSsh.Core
{
    public static class CoreSetup
    {
        public static void SetupContainer(AppSetup appSetup)
        {

            appSetup.ContainerBuilder.RegisterType<MainPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<LoginViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<HomeTabbedPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<HostManagerPageViewModel>().AsSelf();

            appSetup.ContainerBuilder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<RepositoryBase>().As<IRepository>();
            appSetup.ContainerBuilder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<SQLiteFactory>().As<ISQLiteFactory>().SingleInstance();
            var container = appSetup.Build();
        }
    }
}
