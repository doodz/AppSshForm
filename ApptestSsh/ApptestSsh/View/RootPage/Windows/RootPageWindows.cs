using System.Collections.Generic;
using System.Collections.ObjectModel;
using ApptestSsh.Core.View.RootPage.MenuItem;
using ApptestSsh.Core.View.Settings;
using Doods.StdFramework.Navigation;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.RootPage.Windows
{
    public class RootPageWindows : MasterDetailPage
    {
        private Dictionary<AppPage, Page> _pages = new Dictionary<AppPage, Page>();
        private readonly MenuPageUWP _menu;
        private DeepLinkPage _page;
        public static bool IsDesktop { get; set; }
        public RootPageWindows()
        {

            var items = new ObservableCollection<MenuItem.MenuItem>
            {
                new MenuItem.MenuItem { Name = "Home", Icon = "menu_feed.png", Page = AppPage.Home },
                new MenuItem.MenuItem { Name = "Shell", Icon = "menu_sessions.png", Page = AppPage.Shell },    
                new MenuItem.MenuItem { Name = "Settings", Icon = "menu_settings.png", Page = AppPage.Settings }
            };

            _menu = new MenuPageUWP();
            _menu.MenuList.ItemsSource = items;

            _menu.MenuList.ItemSelected += (sender, args) =>
            {
                if (_menu.MenuList.SelectedItem == null)
                    return;

                Device.BeginInvokeOnMainThread(() =>
                {
                    NavigateAsync(((MenuItem.MenuItem)_menu.MenuList.SelectedItem).Page);
                    if (!IsDesktop)
                        IsPresented = false;
                });
            };

            Master = _menu;

            //_menu.MenuList.SelectedItem = items.First();

            NavigateAsync(AppPage.Home);
            Title = "Home";
        }

        public void NavigateAsync(AppPage menuId)
        {
            Page newPage = null;
            if (!_pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {

                    //case AppPage.Sessions://sessions
                    //    _pages.Add(menuId, new EvolveNavigationPage(new SessionsPage()));
                    //    break;

                    case AppPage.Home:
                        newPage = new DoodsNavigationPage(new HomeTabbedPage.HomeTabbedPage());
                        break;
                    case AppPage.HostManager:
                        newPage = new DoodsNavigationPage(new HostManagerPage.HostManagerPage());
                        break;
                    case AppPage.Shell:
                        newPage = new DoodsNavigationPage(new ShellPage.ShellPage());
                        break;
                    case AppPage.Login:
                        newPage = new DoodsNavigationPage(new Login.LoginPage());
                        break;
                    case AppPage.Settings:
                        newPage = new DoodsNavigationPage(new SettingsPage());
                        break;
                }
            }

            if (newPage == null)
                newPage = _pages[menuId];

            if (newPage == null)
                return;

            Detail = newPage;
            //await Navigation.PushAsync(newPage);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Helpers.Settings.Current.FirstRun)
            {
                //TODO doods : go to login page whiout navigation .
                //MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
                //await NavigationService.GotoLoginModal();
            }
        }
    }
}
