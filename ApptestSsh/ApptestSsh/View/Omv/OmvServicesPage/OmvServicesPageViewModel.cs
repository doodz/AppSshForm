using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Omv.OmvServices
{
    public class OmvServicesPageViewModel : LocalViewModel
    {


        public ObservableRangeCollection<Service> Services { get; }
        private SystemInfo _systemInfoTmp;
        private Service _selectedService;

        public ICommand RefreshCommand { get; }
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public Service SelectedService
        {
            get => _selectedService;
            set => SetProperty(ref _selectedService, value);
        }

        public SystemInfo SystemInfo
        {
            get => _systemInfoTmp;
            set => SetProperty(ref _systemInfoTmp, value);
        }

        public ICommand GotoRrdPage { get; private set; }
        public ICommand GotoOmvFileSystemsPage { get; private set; }

        public OmvServicesPageViewModel(ILogger logger) : base(logger)
        {
            Services = new ObservableRangeCollection<Service>();
            RefreshCommand = new Command(async () => await Load());
            GotoRrdPage = new Command(async () => await NavigationService.GotoRddPage());
            GotoOmvFileSystemsPage = new Command(async () => await NavigationService.GotoOmvFileSystemsPage());
            //SystemInfoTmp = new SystemInfo();
        }

        protected override async Task Load()
        {
            using (new RunWithBool(val => { IsRefreshing = val; }))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                if (!ssh.IsConnected() && !ssh.CanConnect())
                {
                    Logger.Info($"{Title} :can't connect");
                    if (ssh.Host != null)
                        Logger.Info($"{Title} :host is null");
                    return;
                }

                await GetSystemInformation(ssh);
                await GetServices(ssh);

            }
        }

        private async Task GetSystemInformation(ISshService ssh)
        {
            var cmd = SystemService.CreateSystemInformationCommand();
            var systemInfoDatas = await new OmvRpcQuery<List<SystemInfoData>>(ssh, cmd).RunAsync(Token);
            var SystemInfoTmp = new SystemInfo();
            foreach (var systemInfoData in systemInfoDatas)
            {
                switch (systemInfoData.Index)
                {

                    case 0:
                        SystemInfoTmp.HostName = systemInfoData;
                        break;
                    case 1:
                        SystemInfoTmp.Version = systemInfoData;
                        break;
                    case 2:
                        SystemInfoTmp.Processor = systemInfoData;
                        break;
                    case 3:
                        SystemInfoTmp.Kernel = systemInfoData;
                        break;
                    case 4:
                        SystemInfoTmp.SystemTime = systemInfoData;
                        break;
                    case 5:
                        SystemInfoTmp.Uptime = systemInfoData;
                        break;
                    case 6:
                        SystemInfoTmp.LoadAverage = systemInfoData;
                        break;
                    case 7:
                        SystemInfoTmp.CpuUsage = systemInfoData;
                        break;
                    case 8:
                        SystemInfoTmp.MemoryUsage = systemInfoData;
                        break;
                }

            }
            SystemInfo = SystemInfoTmp;
        }

        private async Task GetServices(ISshService ssh)
        {
            var cmd = SystemService.CreateStatusServicesCommand();
            var res = await new OmvRpcQuery<CountResultReturn<Service>>(ssh, cmd).RunAsync(Token);
            Services.ReplaceRange(res.Data);
        }
    }
}