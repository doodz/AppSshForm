using Doods.StdFramework.Mvvm;
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
    }
}