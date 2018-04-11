using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ApptestSsh.Core.View.RootPage.MenuItem;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Doods.StdFramework.Views.NavigationView), typeof(ApptestSsh.Droid.Renderer.NavigationViewRenderer))]
namespace ApptestSsh.Droid.Renderer
{
    public class NavigationViewRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<Doods.StdFramework.Views.NavigationView, NavigationView>
    {
        NavigationView navView;
        ImageView profileImage;
        TextView profileName;
        protected override void OnElementChanged(ElementChangedEventArgs<Doods.StdFramework.Views.NavigationView> e)
        {

            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;


            var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
            navView = view.JavaCast<NavigationView>();


            navView.NavigationItemSelected += NavView_NavigationItemSelected;

            //Settings.Current.PropertyChanged += SettingsPropertyChanged;
            SetNativeControl(navView);

            //var header = navView.GetHeaderView(0);
            //profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);
            //profileName = header.FindViewById<TextView>(Resource.Id.profile_name);

            //profileImage.Click += (sender, e2) => NavigateToLogin();
            //profileName.Click += (sender, e2) => NavigateToLogin();

            //UpdateName();
            //UpdateImage();

            navView.SetCheckedItem(Resource.Id.nav_home);
        }

        void NavigateToLogin()
        {
            //if (Settings.Current.IsLoggedIn)
            //    return;
            //var logger = AppContainer.Container.Resolve<ILogger>();
            //logger.TrackPage(AppPage.Login.ToString(), "navigation");
            //MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
        }

        void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(Settings.Current.Email))
            //{
            //    UpdateName();
            //    UpdateImage();
            //}
        }

        void UpdateName()
        {
           // profileName.Text = Settings.Current.UserDisplayName;
        }

        void UpdateImage()
        {
            //Koush.UrlImageViewHelper.SetUrlDrawable(profileImage, Settings.Current.UserAvatar, Resource.Drawable.profile_generic);
        }

        public override void OnViewRemoved(Android.Views.View child)
        {
            base.OnViewRemoved(child);
            navView.NavigationItemSelected -= NavView_NavigationItemSelected;
            //Settings.Current.PropertyChanged -= SettingsPropertyChanged;
        }

        IMenuItem previousItem;

        void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {


            if (previousItem != null)
                previousItem.SetChecked(false);

            navView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            int id = 0;
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_home:
                    id = (int)AppPage.Home;
                    break;
                case Resource.Id.nav_hostManager:
                    id = (int)AppPage.HostManager;
                    break;
                case Resource.Id.nav_Shell:
                    id = (int)AppPage.Shell;
                    break;
                //case Resource.Id.nav_login:
                //    id = (int)AppPage.Login;
                //    break;
                case Resource.Id.nav_settings:
                    id = (int)AppPage.Settings;
                    break;
                
            }
            this.Element.OnNavigationItemSelected(new Doods.StdFramework.Views.NavigationItemSelectedEventArgs
            {

                Index = id
            });
        }


    }
}