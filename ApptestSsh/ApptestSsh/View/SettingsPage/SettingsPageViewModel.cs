using ApptestSsh.Core.View.Settings;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.SettingsPage
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<SettingsMenuItem> AboutItems { get; } = new ObservableRangeCollection<SettingsMenuItem>();
        public ObservableRangeCollection<SettingsMenuItem> TechnologyItems { get; } = new ObservableRangeCollection<SettingsMenuItem>();
        public SettingsPageViewModel(ILogger logger) : base(logger)
        {
            AboutItems.AddRange(new[]
            {
                new SettingsMenuItem { Name = "Created by Xamarin with <3", Command=LaunchBrowserCommand, Parameter="http://www.xamarin.com" },
                new SettingsMenuItem { Name = "Open source on GitHub!", Command=LaunchBrowserCommand, Parameter="https://github.com/doodz/AppSshForm"},
                new SettingsMenuItem { Name = "License GPL 3", Command=LaunchBrowserCommand, Parameter="https://opensource.org/licenses/GPL-3.0/"}
            });

            TechnologyItems.AddRange(new[]
            {
                new SettingsMenuItem { Name = "PCL Storage", Command=LaunchBrowserCommand, Parameter="https://github.com/dsplaisted/PCLStorage"},
                new SettingsMenuItem { Name = "Settings Plugin", Command=LaunchBrowserCommand, Parameter="https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Settings"},
                new SettingsMenuItem { Name = "sqlite-net-pcl", Command=LaunchBrowserCommand, Parameter="https://github.com/praeclarum/sqlite-net"},
                new SettingsMenuItem { Name = "Autofac", Command=LaunchBrowserCommand, Parameter="https://github.com/autofac/Autofac"},
                new SettingsMenuItem { Name = "NLog", Command=LaunchBrowserCommand, Parameter="https://github.com/NLog/NLog/"},
                new SettingsMenuItem { Name = "SSH.NET", Command=LaunchBrowserCommand, Parameter="https://github.com/sshnet/SSH.NET/"},
                new SettingsMenuItem { Name = "Rg.Plugins.Popup", Command=LaunchBrowserCommand, Parameter="https://github.com/rotorgames/Rg.Plugins.Popup/"},
                new SettingsMenuItem { Name = "Xamarin.Forms", Command=LaunchBrowserCommand, Parameter="http://xamarin.com/forms"}
            });
        }

        private ICommand _launchBrowserCommand;
        public ICommand LaunchBrowserCommand =>
            _launchBrowserCommand ?? (_launchBrowserCommand = new Command<string>(async (t) => await ExecuteLaunchBrowserAsync(t)));

        async Task ExecuteLaunchBrowserAsync(string arg)
        {
            if (IsBusy)
                return;

            if (!arg.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !arg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                arg = "http://" + arg;

            Logger.Track(DoodsLoggerKeys.LaunchedBrowser, "Url", arg);
            Device.OpenUri(new Uri(arg));
        }
    }
}
