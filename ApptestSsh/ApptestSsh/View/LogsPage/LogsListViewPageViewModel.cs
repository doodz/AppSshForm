using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using PCLStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace ApptestSsh.Core.View.LogsPage
{
    public class LogsListViewPageViewModel : LocalListViewModel<FileInfoBean>
    {

        public static string LocalLogsFolder = "LogsFiles";


        public LogsListViewPageViewModel(ILogger logger) : base(logger)
        {

        }

        protected override async Task RefreshData()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();

            await GetLogsList(ssh);
        }

        private async Task GetLogsList(ISshService client)
        {
            IsBusyList = true;
            using (new RunWithBusyCount(this))
            {
                var files = await GetLogsList(client, GetLogsListQuery.Path);
                Items.ReplaceRange(files);
            }
            IsBusyList = false;
        }

        private async Task<IEnumerable<FileInfoBean>> GetLogsList(ISshService client, string path)
        {
            var lst = new List<FileInfoBean>();
            var files = await new GetListFileBaseQuery(client, path).RunAsync(Token);

            foreach (var fileinfo in files)
            {

                if (fileinfo.IsFolder)
                {
                    var res = await GetLogsList(client, fileinfo.FullPath);
                    lst.AddRange(res);

                }
                else if (fileinfo.Name.EndsWith(".log") || fileinfo.Name.EndsWith(".warn") ||
                         fileinfo.Name.EndsWith(".info"))
                {
                    lst.Add(fileinfo);
                }
                else if (!fileinfo.Name.Contains("."))
                {
                    lst.Add(fileinfo);
                }

            }
            return lst;
        }


        /// <summary>
        /// There is a bug.
        /// When i select an item, xamarin.forms set selecteditem two time.
        /// </summary>
        private bool _onDisplayAction;

        public override async Task DisplayActionItemTapped()
        {
            if (SelectedItem == null || _onDisplayAction) return;


            using (new RunWithBool(val => { _onDisplayAction = val; }))
            {
                var action =
                    await Application.Current.MainPage.DisplayActionSheet(SelectedItem.Name, "Cancel", null, "Show",
                        "Download");

                switch (action)
                {
                    case "Show":

                        break;
                    case "Download":

                        break;
                    default:
                        break;
                }
            }
        }


        private async Task GetLogFile(ISshService client, FileInfoBean fileinfo)
        {
            var sftpclient = new ClientSftp(client.Host.HostName, client.Host.UserName, client.Host.Password);
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(LocalLogsFolder,
                CreationCollisionOption.OpenIfExists);

            var localfile = await folder.CreateFileAsync(fileinfo.Name,
                CreationCollisionOption.ReplaceExisting);
            var file = await sftpclient.GetFile(fileinfo.FullPath, localfile);

        }
    }
}
