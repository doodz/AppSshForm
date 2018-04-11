using Doods.StdFramework.Mvvm;
using System;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.AboutPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ViewPage<AboutPageViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            await DisplayAlert("Credits",
                "AppSshForm was handcrafted by Doods.\n\n" +
                "Development:\n" +
                "Doods\n" +
                "\n" +
                "Design:\n" +
                "Doods\n" +
                "\n" +
                "Testing:\n" +
                "Doods\n" +
                "\n" +
                "Many thanks to:\n" +
                "Doods\n" +
                "\n" +
                "...and of course you! <3", "OK");
        }
    }
}