using System;
using System.Linq;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.HomeTabbedPage;
using ApptestSsh.Core.View.HostManagerPage;
using ApptestSsh.Core.View.Login;
using ApptestSsh.Core.View.RootPage.Android;
using ApptestSsh.Core.View.RootPage.Windows;
using ApptestSsh.Core.View.ShellPage;
using Doods.StdFramework.Navigation;
using Xamarin.Forms;

namespace ApptestSsh.Core.Services
{
    public static class NavigationService
    {
        private static bool _isNavigating;

        public static INavigation Navigation { get; set; }

        public static Page CurrentPage
        {
            get { return Navigation.NavigationStack.FirstOrDefault(); }
        }

        public static Page CurrentModalPage
        {
            get { return Navigation.ModalStack.FirstOrDefault(); }
        }

        public static void RemovePageFromHistory(Type type)
        {
            var last = Navigation.NavigationStack.ToList().First(p => p.GetType() == type);
            Navigation.RemovePage(last);
        }

        public static void ClearHistory()
        {
            foreach (var page in Navigation.NavigationStack.ToList())
                Navigation.RemovePage(page);
        }

        private static async Task PushAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PushAsync(page, animate);
            _isNavigating = false; 
        }

        private static async Task PushModalAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PushModalAsync(page, animate);
            _isNavigating = false;
        }

        private static async Task PopAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopAsync(animate);
            _isNavigating = false;
        }

        private static async Task PopModalAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopModalAsync(animate);
            _isNavigating = false;
        }

        private static async Task PopToRootAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopToRootAsync(animate);
            _isNavigating = false;
        }

        public static async Task GoBackToRoot()
        {
            await PopToRootAsync(Navigation);
        }

        public static Task GotoLoginModal()
        {
            return PushModalAsync(Navigation,new DoodsNavigationPage( new LoginPage()));
        }

        public static Task GoToLogin()
        {
            return PushAsync(Navigation, new LoginPage());
        }

        public static Task GoToLogin(Host host)
        {
            return PushAsync(Navigation, new LoginPage(host));
        }
        public static Task GoToLoginModal(Host host)
        {
            return PushModalAsync(Navigation, new LoginPage(host));
        }
        public static Task GoToHome()
        {
            return PushAsync(Navigation, new MainPage());
        }

        public static Task GoToHomeTabbed()
        {
            return PushAsync(Navigation, new HomeTabbedPage());
        }

        public static Task GoToHostManagerPage()
        {
            return PushAsync(Navigation, new HostManagerPage());
        }

        public static Task GoToModalHostManagerPage()
        {
            return PushModalAsync(Navigation, new HostManagerPage());
        }

        public static Task GoToShellPage()
        {
            return PushAsync(Navigation, new ShellPage());
        }
        
        public static Task GoBack()
        {
            return PopAsync(Navigation);
        }


       

        public static Task GoBackModal()
        {
            return PopModalAsync(Navigation);
        }
        public static Task GoToRootPageWindows()
        {
            return PushAsync(Navigation, new RootPageWindows());
        }
        public static Task GoToRootPageAndroid()
        {
            return PushAsync(Navigation, new RootPageAndroid());
        }
    }

}
