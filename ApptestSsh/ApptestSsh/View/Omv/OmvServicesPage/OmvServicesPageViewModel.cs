using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Omv.OmvServices
{
    public class OmvPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Service> Services { get; }
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

        public OmvPageViewModel(ILogger logger) : base(logger)
        {
            Services = new ObservableRangeCollection<Service>();
            RefreshCommand = new Command(async () => await Load());
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


                var cmd = SystemService.CreateStatusServicesCommand();
                var res = await new OmvRpcQuery<CountResultReturn<Service>>(ssh, cmd).RunAsync(Token);

                Services.ReplaceRange(res.Data);
            }
        }
    }
}