using ApptestSsh.Core.View.RootPage.Windows;
using Windows.System.Profile;

namespace ApptestSsh.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {

            InitializeComponent();
            RootPageWindows.IsDesktop = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
            LoadApplication(new Core.App());

        }
    }
}
