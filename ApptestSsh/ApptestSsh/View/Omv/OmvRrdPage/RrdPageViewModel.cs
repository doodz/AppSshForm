﻿using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Queries;
using Omv.Rpc.StdClient.Ssh.Queries;
using PCLStorage;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Omv.OmvRrdPage
{
    public class RrdPageViewModel : LocalListViewModel<RddGraphItem>
    {
        public static string RrdFolder = "RrdFiles";

        public ICommand MkGraphCommand { get; }

        public RrdPageViewModel(ILogger logger) : base(logger)
        {
            MkGraphCommand = new Command(async () => await MkGraph());

        }

        protected override async Task RefreshData()
        {
            await MkGraph();
        }

        private async Task MkGraph()
        {
            Items.Clear();
            using (new RunWithBool(val => { IsBusyList = val; }))
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
                try
                {
                    var file = await folder.CreateFileAsync(l.Name,
                        CreationCollisionOption.ReplaceExisting);
                    var fileStream = await sftpclient.GetFile(l.FullName, file);
                    fileStream.Dispose();
                    //var gr = new RddGraphItem() { Name = l.Name, Description = l.LastWriteTime.ToString(), ImageUrl = file.Path, ImageSource = ImageSource.FromFile(file.Path) };
                    var gr = new RddGraphItem()
                    {
                        Name = l.Name,
                        Description = l.LastWriteTime.ToString(),
                        ImageUrl = file.Path,
                        ImageSource = null
                    };

                    Items.Add(gr);
                }
                catch (Exception ex)
                {
                    //TODO THE change that or display error.
                    var msg = ex.Message;
                }
            }



        }

        protected override async Task Load()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(RrdFolder,
                CreationCollisionOption.OpenIfExists);
            await MkGraph();

        }
    }

    public class RddGraphItem : ObservableObject, IName
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}