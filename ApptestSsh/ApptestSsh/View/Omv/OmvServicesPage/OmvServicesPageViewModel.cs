using ApptestSsh.Core.View.Base;
using ApptestSsh.Core.View.PopupPages;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdFramework.Views.PopupPages;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Services;
using Rg.Plugins.Popup.Extensions;
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
        public ICommand GotoPopUpPage { get; private set; }
        public OmvServicesPageViewModel(ILogger logger) : base(logger)
        {
            Services = new ObservableRangeCollection<Service>();
            RefreshCommand = new Command(async () => await Load());
            GotoRrdPage = new Command(async () => await NavigationService.GotoRddPage());
            GotoOmvFileSystemsPage = new Command(async () => await NavigationService.GotoOmvFileSystemsPage());
            GotoPopUpPage = new Command(ShowPopUpPage);
            //SystemInfoTmp = new SystemInfo();
        }

        private async void ShowPopUpPage(object obj)
        {


            await LaunchTextInputPopup();


            //var page = new MyPopupPage();
            //await NavigationService.Navigation.PushPopupAsync(page);
        }


        private async Task<string> LaunchTextInputPopup()
        {
            // create the TextInputView
            var inputView = new TestContentView(
                "What's your name?", "enter here...",
                "Ok", "Ops! Can't leave this empty!");

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(
                        ((TestContentView)sender).TextInputResult))
                    {

                        ((TestContentView)sender)
                            .IsValidationLabelVisible = false;

                        // update the page completion source
                        popup.PageClosedTaskCompletionSource
                            .SetResult(
                                ((TestContentView)sender)
                                .TextInputResult);
                    }
                    else
                    {

                        ((TestContentView)sender)
                            .IsValidationLabelVisible = true;
                    }
                };

            // Push the page to Navigation Stack
            await NavigationService.Navigation.PushPopupAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await NavigationService.Navigation.PopPopupAsync();

            // return user inserted text value
            return result;
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
            var systemInfoTmp = new SystemInfo();
            foreach (var systemInfoData in systemInfoDatas)
            {
                switch (systemInfoData.Index)
                {

                    case 0:
                        systemInfoTmp.HostName = systemInfoData;
                        break;
                    case 1:
                        systemInfoTmp.Version = systemInfoData;
                        break;
                    case 2:
                        systemInfoTmp.Processor = systemInfoData;
                        break;
                    case 3:
                        systemInfoTmp.Kernel = systemInfoData;
                        break;
                    case 4:
                        systemInfoTmp.SystemTime = systemInfoData;
                        break;
                    case 5:
                        systemInfoTmp.Uptime = systemInfoData;
                        break;
                    case 6:
                        systemInfoTmp.LoadAverage = systemInfoData;
                        break;
                    case 7:
                        systemInfoTmp.CpuUsage = systemInfoData;
                        break;
                    case 8:
                        systemInfoTmp.MemoryUsage = systemInfoData;
                        break;
                }

            }
            SystemInfo = systemInfoTmp;
        }

        private async Task GetServices(ISshService ssh)
        {
            var cmd = SystemService.CreateStatusServicesCommand();
            var res = await new OmvRpcQuery<CountResultReturn<Service>>(ssh, cmd).RunAsync(Token);
            Services.ReplaceRange(res.Data);
        }
    }
}