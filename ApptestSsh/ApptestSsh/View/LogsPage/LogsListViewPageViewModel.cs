using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdFramework.Views.PopupPages;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using PCLStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.LogsPage
{
    public class LogsListViewPageViewModel : LocalListViewModel<FileInfoBean>
    {
        /// <summary>
        /// Local folder where logs files are stored.
        /// </summary>
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

        /// <summary>
        /// Get all logs file on the server by ssh.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path">Path on server who we need get for find logs.</param>
        /// <returns>Une liste de FileSystemInfo.</returns>
        private async Task<IEnumerable<FileInfoBean>> GetLogsList(ISshService client, string path)
        {
            var lst = new List<FileInfoBean>();
            var files = await new GetListFileBaseQuery(client, path).RunAsync(Token);

            foreach (var fileinfo in files)
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

            var ssh = AppContainer.Container.Resolve<ISshService>();
            using (new RunWithBool(val => { _onDisplayAction = val; }))
            {
                var action =
                    await Application.Current.MainPage.DisplayActionSheet(SelectedItem.Name, "Cancel", null, "Show",
                        "Download");

                switch (action)
                {
                    case "Show":
                        var localFile = await GetLogFile(ssh, SelectedItem);
                        await NavigationService.GoToContentLogPage(localFile);
                        //await ShowLogFile(localFile);

                        break;
                    case "Download":
                        await DownloadFile(ssh, SelectedItem);
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task<IFile> DownloadFile(ISshService client, FileInfoBean fileinfo)
        {
            var fileHelper = AppContainer.Container.Resolve<IFileHelper>();

            var sftpclient = new ClientSftp(client.Host.HostName, client.Host.UserName, client.Host.Password);
            var rootFolder = await FileSystem.Current.GetFolderFromPathAsync(fileHelper.GetDownloadPath());
            var folder = await rootFolder.CreateFolderAsync(LocalLogsFolder,
                CreationCollisionOption.OpenIfExists);

            var localfile = await folder.CreateFileAsync(fileinfo.Name,
                CreationCollisionOption.ReplaceExisting);
            var file = await sftpclient.GetFile(fileinfo.FullPath, localfile);

            var uri = new Uri(localfile.Path);

            //Device.OpenUri(uri);



            //test

            //var task = CrossHttpTransfers.Current.Download(localfile.Path);
            //task.PropertyChanged += (sender, args) =>
            //{
            //    //if (task.Status != nameof(IHttpStatus.Task))
            //    //    return;

            //    if (task.Status == Plugin.HttpTransferTasks.TaskStatus.Completed)
            //    {
            //        var local = task.LocalFilePath; // move this file appropriately here
            //    }
            //};
            //WebClient client = new WebClient();
            //client.DownloadFile(link, documentsPath);
            //ViewPDF(documentsPath, filename);

            //test
            return localfile;
        }

        private async Task<IFile> GetLogFile(ISshService client, FileInfoBean fileinfo)
        {
            var sftpclient = new ClientSftp(client.Host.HostName, client.Host.UserName, client.Host.Password);
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(LocalLogsFolder,
                CreationCollisionOption.OpenIfExists);

            var localfile = await folder.CreateFileAsync(fileinfo.Name,
                CreationCollisionOption.ReplaceExisting);
            var file = await sftpclient.GetFile(fileinfo.FullPath, localfile);

            return localfile;
        }

        private ShowFileContentView _showFileContentView;
        private InputAlertDialogBase<ProgressContentViewState> _inputAlertDialogBase;

       

        private async Task ShowLogFileBk(IFile localFile)
        {
            // create the TextInputView
            _showFileContentView = new ShowFileContentView(localFile);
            // create the Transparent Popup Page
            // of type string since we need a string return
            _inputAlertDialogBase = new InputAlertDialogBase<ProgressContentViewState>(_showFileContentView);

            // subscribe to the TextInputView's Button click event
            _showFileContentView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (sender is ShowFileContentView view)
                        _inputAlertDialogBase.PageClosedTaskCompletionSource
                            .SetResult(ProgressContentViewState.Closed);
                };

            // Push the page to Navigation Stack
            await NavigationService.Navigation.PushPopupAsync(_inputAlertDialogBase);

            // await for the user to enter the text input
            var result = await _inputAlertDialogBase.PageClosedTask;

            // Pop the page from Navigation Stack
            await NavigationService.Navigation.PopPopupAsync();
            _showFileContentView = null;
            _inputAlertDialogBase = null;
            // return user inserted text value
            //return result;
        }
    }
}