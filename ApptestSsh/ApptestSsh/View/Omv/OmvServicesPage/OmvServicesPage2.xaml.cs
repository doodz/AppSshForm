﻿using Doods.StdFramework.Mvvm;
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
                case Device.WPF:
                case Device.UWP:
                    ToolbarItems.Add(new ToolbarItem
                    {
                        Text = "Refresh",
                        Icon = "Assets/ic_refresh_black_24dp_2x.png",
                        Command = ViewModel.RefreshCommand
                    });
                    break;
            }
            var res = Device.RuntimePlatform == Device.Android ? "ic_dns_black_24dp.png" : "Assets/ic_dns_black_24dp_1x.png";

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Rrd",
                Icon = res,
                Command = ViewModel.GotoRrdPage
            });
            res = Device.RuntimePlatform == Device.Android ? "ic_dns_black_24dp.png" : "Assets/ic_dns_black_24dp_1x.png";
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "File systems",
                Icon = res,
                Command = ViewModel.GotoOmvFileSystemsPage
            });
            res = Device.RuntimePlatform == Device.Android ? "ic_storage_black_24dp.png" : "Assets/ic_storage_black_24dp_1x.png";
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Shereds folders",
                Icon = res,
                Command = ViewModel.GotoSheredsFolders
            });
        }
    }
}