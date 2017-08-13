using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.CommandPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandListViewPage : ViewPage<CommandListViewPageViewModel>
    {
        public CommandListViewPage()
        {
            InitializeComponent();

        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.DisplayAction();
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem == null)
            //    return;

            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            ////Deselect Item
            //((ListView)sender).SelectedItem = null;
        }
    }

}