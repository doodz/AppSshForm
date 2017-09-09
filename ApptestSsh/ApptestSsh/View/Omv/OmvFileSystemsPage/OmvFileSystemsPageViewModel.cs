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

namespace ApptestSsh.Core.View.Omv.OmvFileSystemsPage
{
    public class OmvFileSystemsPageViewModel : LocalViewModel
    {
        public ObservableRangeCollection<FileSystem> FileSystems { get; }
        public ICommand RefreshCommand { get; }
        public ICommand MountUmountCmd { get; }
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public OmvFileSystemsPageViewModel(ILogger logger) : base(logger)
        {
            RefreshCommand = new Command(async () => await Load());
            MountUmountCmd = new Command(MountUmount);
            FileSystems = new ObservableRangeCollection<FileSystem>();
        }

        private async void MountUmount(object obj)
        {
            if (obj is FileSystem fileSystem)
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var cmd = fileSystem.Mounted
                    ? FileSystemService.CreateUmountCommand(fileSystem.Devicefile)
                    : FileSystemService.CreateMountCommand(fileSystem.Devicefile);
                var res = await new OmvRpcQuery<object>(ssh, cmd).RunAsync(Token);
                if (res != null)
                {
                    await GetFileSystems(ssh);
                }

            }
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
                await GetFileSystems(ssh);
            }
        }

        private async Task GetFileSystems(ISshService ssh)
        {
            var cmd = FileSystemService.CreateEnumerateFileSystemCommand();
            var res = await new OmvRpcQuery<CountResultReturn<FileSystem>>(ssh, cmd).RunAsync(Token);
            FileSystems.ReplaceRange(res.Data);
        }
    }
}