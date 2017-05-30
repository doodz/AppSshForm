using ApptestSsh.Core.Services;
using Xamarin.Forms;

namespace ApptestSsh.Core
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage();
            NavigationService.Navigation = MainPage.Navigation;
            //MainPage = new ApptestSsh.Core.View.TestUi.MyMasterDetailPage();
            
        }

        protected async override void OnStart()
        {
            var strng = Device.RuntimePlatform;
            await NavigationService.GoToHome();
            //MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
