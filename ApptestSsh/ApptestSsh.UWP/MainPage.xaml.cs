using Windows.System.Profile;
using ApptestSsh.Core.View.RootPage.Windows;

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
