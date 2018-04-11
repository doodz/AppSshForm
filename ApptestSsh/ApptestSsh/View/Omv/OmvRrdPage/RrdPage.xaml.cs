using Doods.StdFramework.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvRrdPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RrdPage : ListViewPage<RrdPageViewModel>
    {
        public RrdPage()
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
                        Command = ViewModel.RefreshDataCommand
                    });
                    break;
            }
            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Text = "Rrd",
            //    Icon = "Assets/ic_settings_applications_black_2x.png",
            //    Command = ViewModel.GotoRrdPage
            //});
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