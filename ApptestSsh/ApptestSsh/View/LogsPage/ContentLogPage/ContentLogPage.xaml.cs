using Doods.StdFramework.Mvvm;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.LogsPage.ContentLogPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentLogPage : ViewPage<ContentLogPageViewModel>
    {

        public ContentLogPage(IFile localFile)
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

            ViewModel.LocalFile = localFile;
            Title = localFile.Name;
        }


    }
}