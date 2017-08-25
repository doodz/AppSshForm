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