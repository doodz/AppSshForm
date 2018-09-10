using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.HostManagerPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostManagerPage : ViewPage<HostManagerPageViewModel>
    {
        public HostManagerPage()
        {
            
            InitializeComponent();
            switch (Device.RuntimePlatform)
            {
                case Device.WPF:
                case Device.UWP:
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

            //if (sender is Host h)
            //{
            //    await DisplayAlert("Selected",h.HostName, "OK");
            //}

            ////Deselect PluginFormsItem
            //((ListView)sender).SelectedItem = null;
        }
    }

}