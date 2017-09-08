using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Queries;
using Omv.Rpc.StdClient.Ssh.Queries;
using PCLStorage;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Omv.OmvRddPage
{
    public class RrdPageViewModel : LocalViewModel
    {
        public static string RrdFolder = "RrdFiles";

        public ICommand MkGraphCommand { get; }
        public bool OnUpdateGraph { get; set; }

        public ObservableRangeCollection<RddGraph> RrdList { get; set; }

        public RrdPageViewModel(ILogger logger) : base(logger)
        {
            MkGraphCommand = new Command(MkGraph);
            RrdList = new ObservableRangeCollection<RddGraph>();
        }

        private async void MkGraph()
        {
            RrdList.Clear();
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

        //497x221
        private async Task GetRrdFiles(ISshService client)
        {
            var sftpclient = new ClientSftp(client.Host.HostName, client.Host.UserName, client.Host.Password);
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(RrdFolder,
                CreationCollisionOption.OpenIfExists);

            var lst = await sftpclient.GetAllFileFromPath(GetRrdListQuery.Path);

            foreach (var l in lst)
            {
                var file = await folder.CreateFileAsync(l.Name,
                    CreationCollisionOption.ReplaceExisting);
                await sftpclient.GetFile(l.FullName, file);
                var gr = new RddGraph() { Title = l.Name, Description = l.LastWriteTime.ToString(), ImageUrl = file.Path, ImageSource = ImageSource.FromFile(file.Path) };
                RrdList.Add(gr);
            }
        }

        protected override async Task Load()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(RrdFolder,
                CreationCollisionOption.OpenIfExists);


        }
    }

    public class RddGraph : ObservableObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}