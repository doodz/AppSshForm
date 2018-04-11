using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.AboutPage
{
    public class AboutPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<AboutMenuItem> AboutItems { get; } = new ObservableRangeCollection<AboutMenuItem>();
        public ObservableRangeCollection<AboutMenuItem> TechnologyItems { get; } = new ObservableRangeCollection<AboutMenuItem>();
        public AboutPageViewModel(ILogger logger) : base(logger)
        {
            AboutItems.AddRange(new[]
            {
                new AboutMenuItem { Name = "Created by Xamarin with <3", Command=LaunchBrowserCommand, Parameter="http://www.xamarin.com" },
                new AboutMenuItem { Name = "Open source on GitHub!", Command=LaunchBrowserCommand, Parameter="https://github.com/doodz/AppSshForm"},
                new AboutMenuItem { Name = "License GPL 3", Command=LaunchBrowserCommand, Parameter="https://opensource.org/licenses/GPL-3.0/"}
            });

            TechnologyItems.AddRange(new[]
            {
                new AboutMenuItem { Name = "PCL Storage", Command=LaunchBrowserCommand, Parameter="https://github.com/dsplaisted/PCLStorage"},
                new AboutMenuItem { Name = "Settings Plugin", Command=LaunchBrowserCommand, Parameter="https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Settings"},
                new AboutMenuItem { Name = "sqlite-net-pcl", Command=LaunchBrowserCommand, Parameter="https://github.com/praeclarum/sqlite-net"},
                new AboutMenuItem { Name = "Autofac", Command=LaunchBrowserCommand, Parameter="https://github.com/autofac/Autofac"},
                new AboutMenuItem { Name = "NLog", Command=LaunchBrowserCommand, Parameter="https://github.com/NLog/NLog/"},
                new AboutMenuItem { Name = "SSH.NET", Command=LaunchBrowserCommand, Parameter="https://github.com/sshnet/SSH.NET/"},
                new AboutMenuItem { Name = "Rg.Plugins.Popup", Command=LaunchBrowserCommand, Parameter="https://github.com/rotorgames/Rg.Plugins.Popup/"},
                new AboutMenuItem { Name = "Microcharts", Command=LaunchBrowserCommand, Parameter="https://github.com/aloisdeniel/Microcharts"},
                new AboutMenuItem { Name = "httptransfertasks", Command=LaunchBrowserCommand, Parameter="https://github.com/aritchie/httptransfertasks"},
                new AboutMenuItem { Name = "Xamarin.Forms", Command=LaunchBrowserCommand, Parameter="http://xamarin.com/forms"}
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
