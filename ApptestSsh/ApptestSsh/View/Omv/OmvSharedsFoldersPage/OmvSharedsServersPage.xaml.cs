﻿using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvSharedsFoldersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OmvSharedsServersPage : ViewPage<OmvSharedsServersViewModel>
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
    }
}