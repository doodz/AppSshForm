using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Queries;
using Omv.Rpc.StdClient.Ssh.Queries;
using PCLStorage;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Omv.OmvRddPage
{
    public class RrdPageViewModel : BaseViewModel
    {

        public ICommand MkGraphCommand { get; }
        public bool OnUpdateGraph { get; set; }

        protected RrdPageViewModel(ILogger logger) : base(logger)
        {
            MkGraphCommand = new Command(MkGraph);
        }

        private async void MkGraph()
        {
            using (new RunWithBool(val => { OnUpdateGraph = val; }))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var res = await new NuHupQueryWithWaitPid(ssh, GenerateRrdQuery.Query).RunAsync(Token);
                if (res)
                    await GetRrdList(ssh);
            }
        }


        private async Task GetRrdList(ISshService client)
        {

            var res = await new GetRrdListQuery(client).RunAsync(Token);
            await GetRrdFiles(client);

        }


        private async Task GetRrdFiles(ISshService client)
        {
            var sftpclient = new ClientSftp(client.Host.HostName, client.Host.UserName, client.Host.Password);
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync("RrdFiles",
                CreationCollisionOption.OpenIfExists);

            var lst = await sftpclient.GetAllFileFromPath(GetRrdListQuery.Path);

            foreach (var l in lst)
            {
                await sftpclient.GetFile(l.FullName, Path.Combine(folder.Path, l.Name));
            }
        }
    }
}
