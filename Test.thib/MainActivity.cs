using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using ApptestSsh.Core;

namespace Test.thib
{
    [Activity(Label = "Test.thib", Theme = "@style/MainTheme", MainLauncher = true, Icon = "@drawable/icon",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

