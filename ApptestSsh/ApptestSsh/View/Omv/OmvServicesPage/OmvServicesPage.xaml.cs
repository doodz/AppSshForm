using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvServices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OmvServicesPage : ViewPage<OmvServicesPageViewModel>
    {
        public OmvServicesPage()
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

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "OMV",
                Icon = "Assets/ic_dns_black_24dp_1x.png",
                Command = ViewModel.GotoRrdPage
            });

        }
    }
}