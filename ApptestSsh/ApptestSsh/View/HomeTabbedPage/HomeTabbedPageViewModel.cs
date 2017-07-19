using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.Services;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Renci.SshNet.Common;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    public class HomeTabbedPageViewModel : BaseViewModel
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

        public bool _onUpdate;

        public bool OnUpdate
        {
            get => _onUpdate;
            set => SetProperty(ref _onUpdate, value);
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
            ManageHostCmd = new Command(c => NavigationService.GoToModalHostManagerPage());
            ShellCmd = new Command(c => NavigationService.GoToShellPage());
            RefreshCommand = new Command(async () => await ExecuteRefreshCommandAsync());
            GotoLoginCommand = new Command(async () => await NavigationService.GotoLoginModal());
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);

            KillProcessCmd = new Command(Killprocess);
            MountUmountCmd = new Command(MountUmount);
            UpdateCmd = new Command(Update);
            UpdateAllCmd = new Command(UpdateAll);
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
            if(res)
                await GetaptList(ssh);
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
            //var contectSynchro = SynchronizationContext.Current;
            //await new Task(()=>{ }).ConfigureAwait(false);
            if (!ssh.IsConnected() && !ssh.CanConnect())
                return;

            Logger.Info($"{Title} : get VcgencmdQuery");
            var test = new VcgencmdQuery(ssh);
            VcgencmdBean = await test.RunAsync(Token);

            Logger.Info($"{Title} : get SystemInfoQueries");
            SystemBean = await new SystemInfoQueries(ssh).RunAsync(Token);

            var netinfo = await new NetworkInformationQuery(ssh).RunAsync(Token);
            if (NetworkInterfaceInformation.Any())
                NetworkInterfaceInformation.Clear();
            NetworkInterfaceInformation.AddRange(netinfo);

            await GetaptList(ssh);

            Logger.Info($"{Title} : get DiskUsageQuery");
            var diskuse = await new DiskUsageQuery(ssh).RunAsync(Token);
            if (DiskUsage.Any())
                DiskUsage.Clear();
            DiskUsage.AddRange(diskuse);
            var cur = SynchronizationContext.Current;

            Logger.Info($"{Title} : get ProcessesQuery");
            var process = await new ProcessesQuery(ssh, false).RunAsync(Token);

            if (Processes.Any())
                Processes.Clear();
            if (process != null)
                Processes.AddRange(process);
        }


        private async Task GetaptList(ISshService ssh)
        {
            Logger.Info($"{Title} : get AptListQuery");
            var upgradablesBean = await new AptListQuery(ssh).RunAsync(Token);
            if (Upgradables.Any())
                Upgradables.Clear();
            Upgradables.AddRange(upgradablesBean);
        }
    }
}