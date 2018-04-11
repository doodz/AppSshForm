using Doods.StdFramework.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvSharedsFoldersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OmvSharedsServersPage : ListViewPage<OmvSharedsServersViewModel>
    {
        public OmvSharedsServersPage()
        {
            InitializeComponent();
            switch (Device.RuntimePlatform)
            {
                case Device.WinPhone:
                case Device.UWP:
                case Device.WinRT:
                    ToolbarItems.Add(new ToolbarItem
                    {
                        Text = "Refresh",
                        Icon = "Assets/ic_refresh_black_24dp_2x.png",
                        Command = ViewModel.RefreshCommand
                    });
                    break;
            }
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.DisplayActionItemTapped();
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Task.FromResult(0);
            //TODO doods : prendre en compte la selection.
            //if (e.SelectedItem == null)
            //    return;

            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            ////Deselect PluginFormsItem
            //((ListView)sender).SelectedItem = null;
        }
    }
}