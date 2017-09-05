using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : BaseTabbedPage<HomeTabbedPageViewModel>
    {
        public HomeTabbedPage()
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
                Text = "login",
                Icon = "Assets/ic_account_box_black_24dp_1x.png",
                Command = ViewModel.GotoLoginCommand
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "OMV",
                Icon = "Assets/ic_dns_black_24dp_1x.png",
                Command = ViewModel.GotoOmvPage
            });
        }
    }
}