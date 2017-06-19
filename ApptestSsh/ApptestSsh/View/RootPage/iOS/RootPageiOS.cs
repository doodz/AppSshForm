using ApptestSsh.Core.View.RootPage.MenuItem;
using ApptestSsh.Core.View.Settings;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Navigation;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.RootPage.iOS
{
    public class RootPageiOS : TabbedPage
    {

        public RootPageiOS()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            Children.Add(new DoodsNavigationPage(new HomeTabbedPage.HomeTabbedPage()));
            Children.Add(new DoodsNavigationPage(new HostManagerPage.HostManagerPage()));
            Children.Add(new DoodsNavigationPage(new ShellPage.ShellPage()));
            Children.Add(new DoodsNavigationPage(new Login.LoginPage()));
            Children.Add(new DoodsNavigationPage(new SettingsPage()));

            //MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
            //{
            //    switch (p.Page)
            //    {
            //        //case AppPage.Notification:
            //        //    NavigateAsync(AppPage.Notification);
            //        //    await CurrentPage.Navigation.PopToRootAsync();
            //        //    await CurrentPage.Navigation.PushAsync(new NotificationsPage());
            //        //    break;
            //        case AppPage.Events:
            //            NavigateAsync(AppPage.Events);
            //            await CurrentPage.Navigation.PopToRootAsync();
            //            break;
            //        case AppPage.MiniHacks:
            //            NavigateAsync(AppPage.MiniHacks);
            //            await CurrentPage.Navigation.PopToRootAsync();
            //            break;
            //        case AppPage.Session:
            //            NavigateAsync(AppPage.Sessions);
            //            var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession(p.Id);
            //            if (session == null)
            //                break;
            //            await CurrentPage.Navigation.PushAsync(new SessionDetailsPage(session));
            //            break;
            //    }

            //});
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var Logger = AppContainer.Container.Resolve<ILogger>();
            switch (Children.IndexOf(CurrentPage))
            {
                case 0:
                    Logger.TrackPage(AppPage.Home.ToString());
                    break;
                case 1:
                    Logger.TrackPage(AppPage.HostManager.ToString());
                    break;
                case 2:
                    Logger.TrackPage(AppPage.Shell.ToString());
                    break;
                case 3:
                    Logger.TrackPage(AppPage.Login.ToString());
                    break;
                case 4:
                    Logger.TrackPage(AppPage.Settings.ToString());
                    break;
            }
        }

        public void NavigateAsync(AppPage menuId)
        {
            switch ((int)menuId)
            {
                case (int)AppPage.Home: CurrentPage = Children[0]; break;
                case (int)AppPage.HostManager: CurrentPage = Children[1]; break;
                case (int)AppPage.Shell: CurrentPage = Children[2]; break;
                case (int)AppPage.Login: CurrentPage = Children[3]; break;
                case (int)AppPage.Settings: CurrentPage = Children[4]; break;
               
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();



            //if (Settings.Current.FirstRun)
            //{
            //    MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            //}
        }


    }
}

