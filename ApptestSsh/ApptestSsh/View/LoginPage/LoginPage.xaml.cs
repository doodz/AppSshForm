using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.LoginPage;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Login
{

    public partial class LoginPage : ViewPage<LoginViewModel>
    {
       
        public LoginPage()
        {
           
            InitializeComponent();

            var cancel = new ToolbarItem
            {
                Text = "Cancel",
                Command = new Command(async () =>
                {
                    if (ViewModel.IsBusy)
                        return;
                    await NavigationService.GoBackModal();
                })
            };

            
            ToolbarItems.Add(cancel);

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    cancel.Icon = "ic_close_black_24dp.png";
                    break;
                case Device.UWP:
                    cancel.Icon = "Assets/ic_close_black_24dp_1x.png";
                    break;
            }
        }

        public LoginPage(Host host )
        {
            ViewModel.HostObj = host;
            InitializeComponent();
        }
    }
}