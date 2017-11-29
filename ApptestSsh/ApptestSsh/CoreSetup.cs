using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.Base;
using ApptestSsh.Core.View.CommandPage;
using ApptestSsh.Core.View.HomeTabbedPage;
using ApptestSsh.Core.View.HostManagerPage;
using ApptestSsh.Core.View.LoginPage;
using ApptestSsh.Core.View.LogsPage;
using ApptestSsh.Core.View.MainPage;
using ApptestSsh.Core.View.Omv.OmvFileSystemsPage;
using ApptestSsh.Core.View.Omv.OmvRrdPage;
using ApptestSsh.Core.View.Omv.OmvServicesPage;
using ApptestSsh.Core.View.Omv.OmvSharedsFoldersPage;
using ApptestSsh.Core.View.Omv.OmvUpdatePage;
using ApptestSsh.Core.View.RootPage.MasterDetailPage;
using ApptestSsh.Core.View.SettingsPage;
using ApptestSsh.Core.View.ShellPage;
using ApptestSsh.Core.View.UpgradablePage;
using ApptestSsh.Core.View.WelcomeStartPage;
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
            appSetup.ContainerBuilder.RegisterType<ShellPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<SettingsPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<WelcomeStartPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<AddCommandPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<CommandListViewPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<UpgradableListViewPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<RootMasterDetailPageMasterViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<OmvServicesPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<RrdPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<OmvFileSystemsPageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<OmvUpdatePageViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<OmvSharedsServersViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<LocalViewModel>().AsSelf();
            appSetup.ContainerBuilder.RegisterType<LogsListViewPageViewModel>().AsSelf();

            appSetup.ContainerBuilder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<RepositoryBase>().As<IRepository>();
            appSetup.ContainerBuilder.RegisterType<SshService>().As<ISshService>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<SQLiteFactory>().As<ISQLiteFactory>().SingleInstance();
            appSetup.ContainerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            var container = appSetup.Build();
        }
    }
}
