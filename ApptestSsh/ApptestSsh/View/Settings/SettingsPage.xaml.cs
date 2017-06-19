using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.View.MainPage;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ViewPage<SettingsPageViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();

        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
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