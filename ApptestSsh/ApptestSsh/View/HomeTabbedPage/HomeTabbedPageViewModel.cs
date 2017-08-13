using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Doods.StdRepository.Base;
using System;
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
        public ICommand RefreshCommand { get; }
        public ICommand GotoLoginCommand { get; }
        public ICommand KillProcessCmd { get; }
        public ICommand MountUmountCmd { get; }
        public ICommand UpdateCmd { get; }
        public ICommand UpdateAllCmd { get; }
        public ICommand ShowUpgradablesCmd { get; }


        public bool _onUpdate;

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

        public DateTime NextForceRefresh { get; set; }

        public ObservableRangeCollection<UpgradableBean> Upgradables { get; }
        public ObservableRangeCollection<NetworkInterfaceInformationBean> NetworkInterfaceInformation { get; }
        public ObservableRangeCollection<ProcessBean> Processes { get; }
        public ObservableRangeCollection<DiskUsageBean> DiskUsage { get; }

        public HomeTabbedPageViewModel(ILogger logger) : base(logger)
        {
            Title = "Home";
            NetworkInterfaceInformation = new ObservableRangeCollection<NetworkInterfaceInformationBean>();
            DiskUsage = new ObservableRangeCollection<DiskUsageBean>();
            Processes = new ObservableRangeCollection<ProcessBean>();
            Upgradables = new ObservableRangeCollection<UpgradableBean>();
            ManageHostCmd = new Command(c => NavigationService.GoToHostManagerPage());
            ShellCmd = new Command(c => NavigationService.GoToShellPage());
            RefreshCommand = new Command(async () => await ExecuteRefreshCommandAsync());
            GotoLoginCommand = new Command(async () => await NavigationService.GotoLoginModal());
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);

            KillProcessCmd = new Command(Killprocess);
            MountUmountCmd = new Command(MountUmount);
            UpdateCmd = new Command(Update);
            UpdateAllCmd = new Command(UpdateAll);
            ShowUpgradablesCmd = new Command(async () => await NavigationService.GoUpgradableListViewPageModal());
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
            OnUpdate = true;

            var ssh = AppContainer.Container.Resolve<ISshService>();

            //var res = await new NoHupQuery(ssh, UpdateAllQuery.Query).RunAsync(Token);
            //bool isRunningIpd;

            //var delay = 1000;
            //do
            //{
            //    await Task.Delay(delay);
            //    isRunningIpd = await new IsRunningPidQuery(ssh, res).RunAsync(Token);
            //    delay += 1000;
            //    if (delay > 5000)
            //        delay = 1000;
            //} while (isRunningIpd);

            var res = await new NuHupQueryWithWaitPid(ssh, UpdateAllQuery.Query).RunAsync(Token);
            if (res)
                await GetAptList(ssh);
            OnUpdate = false;
        }

        private async void Update(object obj)
        {
            if (!(obj is UpgradableBean pb)) return;

            //var ssh = AppContainer.Container.Resolve<ISshService>();
            //var res = await new KillProcessQuery(ssh, pb.Pid).RunAsync(Token);


            //if (res)
            //{
            //    //Processes.Remove(pb);
            //}
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
            //var res = await new KillProcessQuery(ssh, pb.Pid).RunAsync(Token);
        }

        private async Task ExecuteRefreshCommandAsync()
        {
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
            BusyCount++;
            await Load();
            BusyCount--;
        }

        private async Task DoSomething()
        {
            await Task.Delay(10000);

            await Application.Current.MainPage.DisplayAlert("Alert", "Finished", "OK");
        }

        protected override async Task Load()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();
            if (!ssh.IsInitialised)
                await CheckSshParams(ssh);

            //var contectSynchro = SynchronizationContext.Current;
            //await new Task(()=>{ }).ConfigureAwait(false);
            if (!ssh.IsConnected() && !ssh.CanConnect())
            {
                Logger.Info($"{Title} :can't connect");
                if (ssh.Host != null)
                    Logger.Info($"{Title} :host is null");
                return;
            }
            OnUpdateVcgencmdBean = true;
            Logger.Info($"{Title} : get VcgencmdQuery");
            var test = new VcgencmdQuery(ssh);
            VcgencmdBean = await test.RunAsync(Token);
            
            Logger.Info($"{Title} : get SystemInfoQueries");
            SystemBean = await new SystemInfoQueries(ssh).RunAsync(Token);
            
            var netinfo = await new NetworkInformationQuery(ssh).RunAsync(Token);
            if (NetworkInterfaceInformation.Any())
                NetworkInterfaceInformation.Clear();
            NetworkInterfaceInformation.AddRange(netinfo);

            await GetAptList(ssh);

            await GetDiskUsage(ssh);

            //var cur = SynchronizationContext.Current;

            await GetProcesses(ssh);
            OnUpdateVcgencmdBean = false;
        }



        private async Task GetProcesses(ISshService ssh)
        {
            Logger.Info($"{Title} : get ProcessesQuery");
            var process = await new ProcessesQuery(ssh, false).RunAsync(Token);

            if (Processes.Any())
                Processes.Clear();
            if (process != null)
                Processes.AddRange(process);
        }

        private async Task GetDiskUsage(ISshService ssh)
        {
            Logger.Info($"{Title} : get DiskUsageQuery");
            var diskuse = await new DiskUsageQuery(ssh).RunAsync(Token);
            if (DiskUsage.Any())
                DiskUsage.Clear();
            DiskUsage.AddRange(diskuse);
        }

        private async Task GetAptList(ISshService ssh)
        {
            Logger.Info($"{Title} : get AptListQuery");
            var upgradablesBean = await new AptListQuery(ssh).RunAsync(Token);
            if (Upgradables.Any())
                Upgradables.Clear();
            Upgradables.AddRange(upgradablesBean);
        }
    }
}