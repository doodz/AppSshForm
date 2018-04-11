using Android.App;
using Android.Widget;
using Android.OS;

namespace Omv.Rpc.Android
{
    [Activity(Label = "Omv.Rpc.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

