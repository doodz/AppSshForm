using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvServicesPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OmvServicesPage2 : ViewPage<OmvServicesPageViewModel>
    {
        public OmvServicesPage2()
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
                Text = "Rrd",
                Icon = "Assets/ic_dns_black_24dp_1x.png",
                Command = ViewModel.GotoRrdPage
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "File systems",
                Icon = "Assets/ic_dns_black_24dp_1x.png",
                Command = ViewModel.GotoOmvFileSystemsPage
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Shereds folders",
                Icon = "Assets/ic_dns_black_24dp_1x.png",
                Command = ViewModel.GotoSheredsFolders
            });
        }
    }
}