using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.CommandPage;
using ApptestSsh.Core.View.HomeTabbedPage;
using ApptestSsh.Core.View.HostManagerPage;
using ApptestSsh.Core.View.Login;
using ApptestSsh.Core.View.Omv.OmvRrdPage;
using ApptestSsh.Core.View.Omv.OmvServices;
using ApptestSsh.Core.View.RootPage.MasterDetailPage;
using ApptestSsh.Core.View.RootPage.Windows;
using ApptestSsh.Core.View.ShellPage;
using ApptestSsh.Core.View.UpgradablePage;
using ApptestSsh.Core.View.WelcomeStartPage;
using Doods.StdFramework.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using SettingsPage = ApptestSsh.Core.View.SettingsPage.SettingsPage;

namespace ApptestSsh.Core.Services
{
    public class NavigationService : BaseNavigationService, INavigationService
    {
        public Task GotoLoginModal()
        {
            return PushModalAsync(Navigation, new DoodsNavigationPage(new LoginPage()));
        }

        public Task GoToLogin()
        {
            return PushAsync(Navigation, new LoginPage());
        }

        public Task GoToLogin(Host host)
        {
            return PushAsync(Navigation, new LoginPage(host));
        }

        public Task GoToLoginModal(Host host)
        {
            return PushModalAsync(Navigation, new LoginPage(host));
        }

        public Page GetRootPage()
        {
            return new DoodsNavigationPage(new RootMasterDetailPage());
        }

        public Task GoToRootPage()
        {
            return PushAsync(Navigation, new RootMasterDetailPage());
        }

        public Task GoToHome()
        {
            return PushAsync(Navigation, new MainPage());
        }

        public Task GoToHomeTabbed()
        {
            return PushAsync(Navigation, new HomeTabbedPage());
        }

        public Task GoToHostManagerPage()
        {
            return PushAsync(Navigation, new HostManagerPage());
        }

        public Task GoToModalHostManagerPage()
        {
            return PushModalAsync(Navigation, new HostManagerPage());
        }

        public Task GoToShellPage()
        {
            return PushAsync(Navigation, new ShellPage());
        }

        public Task GoToSettingsPage()
        {
            return PushModalAsync(Navigation, new DoodsNavigationPage(new SettingsPage()));
        }

        public Page GetWelcomeStartPage()
        {
            return new DoodsNavigationPage(new WelcomeStartPage());
        }




        public Task GoToRootPageWindows()
        {
            return PushAsync(Navigation, new RootPageWindows());
        }

        //public Task GoToRootPageAndroid()
        //{
        //    return PushAsync(Navigation, new RootPageAndroid());
        //}

        public Task GoToRootPageWindowsModal()
        {
            return PushModalAsync(Navigation, new RootPageWindows());
        }

        //public Task GoToRootPageAndroidModal()
        //{
        //    return PushModalAsync(Navigation, new RootPageAndroid());
        //}


        public Task GoUpgradableListViewPageModal()
        {
            return PushModalAsync(Navigation, new UpgradableListViewPage());
        }
        public Task GoUpgradableListViewPage()
        {
            return PushAsync(Navigation, new UpgradableListViewPage());
        }
        public Task GoToAddCommandPage()
        {
            return PushAsync(Navigation, new AddCommandPage());
        }

        public Task GoToEditCommandPage(CommandSsh cmd)
        {
            return PushAsync(Navigation, new AddCommandPage(cmd));
        }

        public Task GoToOmvServicesPage()
        {
            return PushAsync(Navigation, new OmvServicesPage());
        }

        public Task GotoRddPage()
        {
            return PushAsync(Navigation, new RrdPage());
        }
    }
}