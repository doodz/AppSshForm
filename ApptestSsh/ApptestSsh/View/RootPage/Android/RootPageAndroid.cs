using System.Collections.Generic;
using System.Threading.Tasks;
using ApptestSsh.Core.View.RootPage.MenuItem;
using ApptestSsh.Core.View.Settings;
using Doods.StdFramework.Navigation;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.RootPage.Android
{
    public class RootPageAndroid : MasterDetailPage
    {
        Dictionary<int, DoodsNavigationPage> _pages;
        DeepLinkPage page;
        bool isRunning = false;
        public RootPageAndroid()
        {
            _pages = new Dictionary<int, DoodsNavigationPage>();
            Master = new MenuPageAndroid(this);


            _pages.Add(0, new DoodsNavigationPage(new HomeTabbedPage.HomeTabbedPage()));
           
            Detail = _pages[0];
            //MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
            //{
            //    page = p;

            //    if (isRunning)
            //        await GoToDeepLink();
            //});
        }

        public async Task NavigateAsync(int menuId)
        {
            DoodsNavigationPage newPage = null;
            if (!_pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {

                    //case AppPage.Sessions://sessions
                    //    _pages.Add(menuId, new EvolveNavigationPage(new SessionsPage()));
                    //    break;

                    case (int)AppPage.Home:
                        newPage = new DoodsNavigationPage(new HomeTabbedPage.HomeTabbedPage());
                        break;
                    case (int)AppPage.HostManager:
                        newPage = new DoodsNavigationPage(new HostManagerPage.HostManagerPage());
                        break;
                    case (int)AppPage.Shell:
                        newPage = new DoodsNavigationPage(new ShellPage.ShellPage());
                        break;
                    case (int)AppPage.Login:
                        newPage = new DoodsNavigationPage(new Login.LoginPage());
                        break;
                    case (int)AppPage.Settings:
                        newPage = new DoodsNavigationPage(new SettingsPage());
                        break;
                }
            }

            if (newPage == null)
                newPage = _pages[menuId];

            if (newPage == null)
                return;

            //if we are on the same tab and pressed it again.
            if (Detail == newPage)
            {
                await newPage.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            //if (Settings.Current.FirstRun)
            //{
            //    MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            //}

            isRunning = true;

            await GoToDeepLink();

        }
        async Task GoToDeepLink()
        {
            if (page == null)
                return;
            var p = page.Page;
            var id = page.Id;
            page = null;
            //switch (p)
            //{
            //    case AppPage.Sessions:
            //        await NavigateAsync((int)AppPage.Sessions);
            //        break;
            //    case AppPage.Session:
            //        await NavigateAsync((int)AppPage.Sessions);
            //        if (string.IsNullOrWhiteSpace(id))
            //            break;

            //        var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession(id);
            //        if (session == null)
            //            break;
            //        await Detail.Navigation.PushAsync(new SessionDetailsPage(session));
            //        break;
            //    case AppPage.Events:
            //        await NavigateAsync((int)AppPage.Events);
            //        break;
            //    case AppPage.MiniHacks:
            //        await NavigateAsync((int)AppPage.MiniHacks);
            //        break;
            //}

        }

    }
}


