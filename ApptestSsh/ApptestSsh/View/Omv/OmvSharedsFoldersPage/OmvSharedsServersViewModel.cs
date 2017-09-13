using ApptestSsh.Core.View.Base;
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

namespace ApptestSsh.Core.View.Omv.OmvSharedsFoldersPage
{
    public class OmvSharedsServersViewModel : LocalViewModel
    {
        public ObservableRangeCollection<SharedFolder> SharedFolders { get; }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }


        public ICommand RefreshCommand { get; }

        public OmvSharedsServersViewModel(ILogger logger) : base(logger)
        {
            RefreshCommand = new Command(async () => await Load());
            SharedFolders = new ObservableRangeCollection<SharedFolder>();
        }


        protected override async Task Load()
        {
            // TODO doods avoir pour utiliser cette librairie
            // using SharpCifs.Smb;
            // await ScanServers();

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
                await GetShareFolders(ssh);
            }
        }

        private async Task GetShareFolders(ISshService ssh)
        {
            var cmd = ShareMgmtService.CreateEnumerateShareCommand();
            var res = await new OmvRpcQuery<CountResultReturn<SharedFolder>>(ssh, cmd).RunAsync(Token);
            SharedFolders.ReplaceRange(res.Data);
        }
    }
}