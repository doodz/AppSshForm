using ApptestSsh.Core.View.Settings;
using Doods.StdFramework.Mvvm;
using System;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.SettingsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ViewPage<SettingsPageViewModel>
    {
        public SettingsPage()
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