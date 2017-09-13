using ApptestSsh.Core.View.Base;
using ApptestSsh.Core.View.Omv.OmvRddPage;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using Omv.Rpc.StdClient.Ssh.Queries;
using PCLStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.LogsPage
{
    public class LogsPageViewModel : LocalViewModel
    {

        public static string LocalLogsFolder = "LogsFiles";

        public ObservableRangeCollection<FileInfoBean> LogsFiles { get; }
        public LogsPageViewModel(ILogger logger) : base(logger)
        {
            LogsFiles = new ObservableRangeCollection<FileInfoBean>();
        }

        protected override async Task Load()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();

            await GetLogsList(ssh);
        }

        private async Task GetLogsList(ISshService client)
        {
            var files = await GetLogsList(client, GetLogsListQuery.Path);
            LogsFiles.ReplaceRange(files);
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
