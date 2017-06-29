﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.Services;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Doods.StdLibSsh.Queries.GroupedQueries;
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

        public ObservableRangeCollection<NetworkInterfaceInformationBean> NetworkInterfaceInformation { get; }
        public ObservableRangeCollection<ProcessBean> Processes { get; }
        public ObservableRangeCollection<DiskUsageBean> DiskUsage { get; }
        public HomeTabbedPageViewModel(ILogger logger) : base(logger)
        {
            Title = "Home";
            NetworkInterfaceInformation = new ObservableRangeCollection<NetworkInterfaceInformationBean>();
            DiskUsage = new ObservableRangeCollection<DiskUsageBean>();
            Processes = new ObservableRangeCollection<ProcessBean>();
            ManageHostCmd = new Command(c =>NavigationService.GoToModalHostManagerPage());
            ShellCmd = new Command(c => NavigationService.GoToShellPage());
            RefreshCommand = new Command(async () => await ExecuteRefreshCommandAsync());
            GotoLoginCommand = new Command(async () => await NavigationService.GotoLoginModal());
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
        }

        async Task ExecuteRefreshCommandAsync()
        {

            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
            BusyCount++;
            await Load();
            BusyCount--;
        }

        private async Task DoSomething()
        {
            await Task.Delay(10000);

            await App.Current.MainPage.DisplayAlert("Alert", "Finished", "OK");

        }

        protected override async Task Load()
        {

            var ssh = AppContainer.Container.Resolve<ISshService>();

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
