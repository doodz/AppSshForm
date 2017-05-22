using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.MainPage
{
    public class MainPageViewModel : BaseViewModel
    {
        private VcgencmdBean _vcgencmdBean;

        public VcgencmdBean VcgencmdBean
        {
            get => _vcgencmdBean;
            set => SetProperty(ref _vcgencmdBean, value);
        }

        private SystemBean _systemBean;

        public SystemBean SystemBean
        {
            get => _systemBean;
            set => SetProperty(ref _systemBean, value);
        }

        public ObservableRangeCollection<NetworkInterfaceInformationBean> NetworkInterfaceInformation { get;}
        public ObservableRangeCollection<ProcessBean> Processes { get;}
        public ObservableRangeCollection<DiskUsageBean> DiskUsage { get;}
        public MainPageViewModel(ILogger logger) : base(logger)
        {
            Title = "Home";
            NetworkInterfaceInformation = new ObservableRangeCollection<NetworkInterfaceInformationBean>();
            DiskUsage = new ObservableRangeCollection<DiskUsageBean>();
            Processes = new ObservableRangeCollection<ProcessBean>();
        }


        private async Task DoSomething()
        {
            await Task.Delay(10000);
            
            await App.Current.MainPage.DisplayAlert("Alert", "Finished", "OK");

        }

        protected override async Task Load()
        {

            var ssh = AppContainer.Container.Resolve<ISshService>();
            var host = new Host
            {
                HostName = "192.168.1.73",
                UserName = "pi",
                Password = "raspberry"
            };

            ssh.Host = host;
            ssh.Initialise();

            Logger.Info($"{Title} : get VcgencmdQuery");
            var test = new VcgencmdQuery(ssh);
            VcgencmdBean = await test.RunAsync(Token);
            Logger.Info($"{Title} : get SystemInfoQueries");
            SystemBean = await new SystemInfoQueries(ssh).RunAsync(Token);

            var netinfo = await new NetworkInformationQuery(ssh).RunAsync(Token);
            if (NetworkInterfaceInformation.Any())
                NetworkInterfaceInformation.Clear();
            NetworkInterfaceInformation.AddRange(netinfo);

            Logger.Info($"{Title} : get DiskUsageQuery");
            var diskuse = await new DiskUsageQuery(ssh).RunAsync(Token);
            if (DiskUsage.Any())
                DiskUsage.Clear();
            DiskUsage.AddRange(diskuse);
            Logger.Info($"{Title} : get ProcessesQuery");
            var process = await new ProcessesQuery(ssh, true).RunAsync(Token);
            if (Processes.Any())
                Processes.Clear();
            if (process != null)
                Processes.AddRange(process);
        }
    }
}
