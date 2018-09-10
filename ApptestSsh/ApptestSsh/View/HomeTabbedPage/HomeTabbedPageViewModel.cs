using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Doods.StdRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    public class HomeTabbedPageViewModel : LocalViewModel
    {
        private VcgencmdBean _vcgencmdBean;
        public ICommand ManageHostCmd { get; }
        public ICommand ShellCmd { get; }
        public ICommand GotoLoginCommand { get; }
        public ICommand GotoOmvPage { get; }

        public ICommand KillProcessCmd { get; }
        public ICommand MountUmountCmd { get; }
        public ICommand UpdateCmd { get; }
        public ICommand UpdateAllCmd { get; }
        public ICommand ShowUpgradablesCmd { get; }

        private bool _isRpi;

        public bool IsRpi
        {
            get => _isRpi;
            set => SetProperty(ref _isRpi, value);
        }

        private bool _onUpdate;

        public bool OnUpdate
        {
            get => _onUpdate;
            set => SetProperty(ref _onUpdate, value);
        }

        private bool _onUpdateVcgencmdBean;

        public bool OnUpdateVcgencmdBean
        {
            get => _onUpdateVcgencmdBean;
            set => SetProperty(ref _onUpdateVcgencmdBean, value);
        }

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

        private bool _haveErrorMessage;

        public bool HaveErrorMessage
        {
            get => _haveErrorMessage;
            set => SetProperty(ref _haveErrorMessage, value);
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public DateTime NextForceRefresh { get; set; }

        public ObservableRangeCollection<UpgradableBean> Upgradables { get; }
        public ObservableRangeCollection<NetworkInterfaceInformationBean> NetworkInterfaceInformation { get; }
        public ObservableRangeCollection<ProcessBean> Processes { get; }
        public ObservableRangeCollection<DiskUsageViewModel> DiskUsage { get; }

        public HomeTabbedPageViewModel(ILogger logger) : base(logger)
        {
            ReloadOnAppearing = true;
            Title = "Home";
            NetworkInterfaceInformation = new ObservableRangeCollection<NetworkInterfaceInformationBean>();
            DiskUsage = new ObservableRangeCollection<DiskUsageViewModel>();
            Processes = new ObservableRangeCollection<ProcessBean>();
            Upgradables = new ObservableRangeCollection<UpgradableBean>();
            ManageHostCmd = new Command(c => NavigationService.GoToHostManagerPage());
            ShellCmd = new Command(c => NavigationService.GoToShellPage());
            RefreshCommand = new Command(async () => await ExecuteRefreshCommandAsync());
            GotoLoginCommand = new Command(async () => await NavigationService.GotoLoginModal());
            GotoOmvPage = new Command(async () => await NavigationService.GoToOmvServicesPage());
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);

            KillProcessCmd = new Command(Killprocess);
            MountUmountCmd = new Command(MountUmount, o => false);
            UpdateCmd = new Command(Update);
            UpdateAllCmd = new Command(UpdateAll);
            ShowUpgradablesCmd = new Command(async () => await NavigationService.GoUpgradableListViewPage());
        }

        private ToolbarItem _omvToolbarItem;

        public override IEnumerable<ToolbarItem> GetToolbarItems()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();
            //CheckSshParams(ssh).Wait();
            //if (ssh.Host != null)
            switch (Device.RuntimePlatform)
            {
                case Device.WPF:
                case Device.UWP:
                    yield return new ToolbarItem
                    {
                        Text = "Refresh",
                        Icon = "Assets/ic_refresh_black_24dp_1x.png",
                        Command = RefreshCommand
                    };
                    break;
            }
            var res = Device.RuntimePlatform == Device.Android ? "ic_storage_black_24dp.png" : "Assets/ic_storage_black_24dp_1x.png";
            yield return new ToolbarItem
            {
                Text = "Manage Host",
                Icon = res,
                Command = ManageHostCmd
            };
            res = Device.RuntimePlatform == Device.Android ? "ic_account_box_black_24dp.png" : "Assets/ic_account_box_black_24dp_1x.png";
            yield return new ToolbarItem
            {
                Text = "login",
                Icon = res,
                Command = GotoLoginCommand
            };
            res = Device.RuntimePlatform == Device.Android ? "ic_dns_black_24dp.png" : "Assets/ic_dns_black_24dp_1x.png";
            if (ssh?.Host != null && ssh.Host.IsOmvServer)
                yield return _omvToolbarItem = new ToolbarItem
                {
                    Text = "OMV",
                    Icon = res,
                    Command = GotoOmvPage
                };
        }

        private async Task CheckSshParams(ISshService ssh)
        {
            var repo = AppContainer.Container.Resolve<IRepository>();
            var list = await repo.GetAllAsync<Host>();
            if (!list.Any())
            {
                var logger = AppContainer.Container.Resolve<ILogger>();
                logger.Info($"{Title} : No host in repository.");
            }

            var host = list.FirstOrDefault(l => l.Id == Helpers.Settings.Current.LastHostId) ?? list.First();

            ssh.Host = host;
            ssh.Initialise();
        }

        private async void UpdateAll(object obj)
        {
            using (new RunWithBool(val => { OnUpdate = val; }))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var res = await new NuHupQueryWithWaitPid(ssh, UpgradeAllQuery.Query).RunAsync(Token);
                if (res)
                    await GetAptList(ssh);
            }
        }

        private async void Update(object obj)
        {
            if (!(obj is UpgradableBean pb)) return;
        }

        private async void Killprocess(object obj)
        {
            if (!(obj is ProcessBean pb)) return;

            var ssh = AppContainer.Container.Resolve<ISshService>();
            var res = await new KillProcessQuery(ssh, pb.Pid).RunAsync(Token);

            if (res)
                Processes.Remove(pb);
        }

        private async void MountUmount(object obj)
        {
            if (!(obj is DiskUsageBean disk)) return;

            var ssh = AppContainer.Container.Resolve<ISshService>();
            var res = await new UmountQuery(ssh, disk.MountedOn).RunAsync(Token);
        }

        private async Task ExecuteRefreshCommandAsync()
        {
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
            using (new RunWithBusyCount(this))
            {
                await Load();
            }
        }

        protected override async Task Load()
        {
            HaveErrorMessage = false;
            ErrorMessage = string.Empty;
            var ssh = AppContainer.Container.Resolve<ISshService>();
            if (!ssh.IsInitialised)
                await CheckSshParams(ssh);
            if (!ssh.IsConnected() && !ssh.CanConnect())
            {
                Logger.Info($"{Title} :can't connect");
                if (ssh.Host != null)
                    Logger.Info($"{Title} :host is null");
                return;
            }


            if (!ssh.IsConnected())
            {
                var res = await ssh.ConnectAsync();
                if (!res)
                {
                    ErrorMessage = $"Unable to connect to {ssh.Host.HostName}";
                    HaveErrorMessage = true;

                    return;
                }
                //var resultatttt = await new LastloginsQuery(ssh).RunAsync(Token);
            }


            IsRpi = ssh.Host.IsRpi;
            if (IsRpi)
                await GetVcgenResults(ssh);

            var t1 = GetSystemInfo(ssh);

            var t2 = GetNetWorkInfo(ssh);

            var t3 = GetAptList(ssh);

            var t4 = GetDiskUsage(ssh);

            ////var cur = SynchronizationContext.Current;

            var t5 = GetProcesses(ssh);

            //Task.WaitAll(new Task[] { /*t1,*/ t2, t3, t4, t5 });
        }

        protected override Task OnInternalAppearing()
        {
            return Task.FromResult(0);
        }

        private async Task GetSystemInfo(ISshService ssh)
        {
            Logger.Info($"{Title} : get {nameof(SystemInfoQueries)}");
            SystemBean = await new SystemInfoQueries(ssh).RunAsync(Token);
        }

        private async Task GetNetWorkInfo(ISshService ssh)
        {
            var netinfo = await new NetworkInformationQuery(ssh).RunAsync(Token);
            //var networkInformationQuery = new NetworkInformationQuery(ssh);
            //await networkInformationQuery.SendCommandAsync(Token);
            //var netinfo = await networkInformationQuery.PaseResultAsync(Token);
            if (netinfo == null) return;
            if (NetworkInterfaceInformation.Any())
                NetworkInterfaceInformation.Clear();
            NetworkInterfaceInformation.AddRange(netinfo);
        }


        private async Task GetVcgenResults(ISshService ssh)
        {
            OnUpdateVcgencmdBean = true;
            Logger.Info($"{Title} : get {nameof(VcgencmdQuery)}");
            var test = new VcgencmdQuery(ssh);

            await test.RunAsync(Token).ContinueWith(res =>
            {
                if (res.IsCanceled) return;

                VcgencmdBean = res.Result;
                if (VcgencmdBean.ArmFrequency == 0 && VcgencmdBean.CoreFrequency == 0 && VcgencmdBean.CoreVolts == 0d &&
                    VcgencmdBean.CpuTemperature == 0d && VcgencmdBean.Version == "n/a")
                {
                    ErrorMessage = "Are you sure that you use a RPI ? I can't get arm and core info.";
                    HaveErrorMessage = true;

                }

                OnUpdateVcgencmdBean = false;
            }, TaskContinuationOptions.NotOnCanceled);
            //}, TaskScheduler.FromCurrentSynchronizationContext());
        }


        private async Task GetProcesses(ISshService ssh)
        {
            Logger.Info($"{Title} : get {nameof(ProcessesQuery)}");
            var process = await new ProcessesQuery(ssh, false).RunAsync(Token);
            if (process == null) return;
            if (Processes.Any())
                Processes.Clear();
            Processes.AddRange(process);
        }

        private async Task GetDiskUsage(ISshService ssh)
        {
            Logger.Info($"{Title} : get {nameof(DiskUsageQuery)}");
            var diskuse = await new DiskUsageQuery(ssh).RunAsync(Token);
            if (diskuse == null) return;
            if (DiskUsage.Any())
                DiskUsage.Clear();
            DiskUsage.AddRange(diskuse.Select(d => new DiskUsageViewModel(d)));
        }

        private async Task GetAptList(ISshService ssh)
        {
            Logger.Info($"{Title} : get {nameof(AptListQuery)}");
            var upgradablesBean = await new AptListQuery(ssh).RunAsync(Token);
            if (upgradablesBean == null) return;
            if (Upgradables.Any())
                Upgradables.Clear();
            Upgradables.AddRange(upgradablesBean);
        }
    }
}