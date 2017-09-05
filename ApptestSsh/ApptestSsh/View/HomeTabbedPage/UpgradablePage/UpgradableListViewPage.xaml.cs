using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.UpgradablePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpgradableListViewPage : ViewPage<UpgradableListViewPageViewModel>
    {


        public UpgradableListViewPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect PluginFormsItem
            ((ListView)sender).SelectedItem = null;
        }
    }
}