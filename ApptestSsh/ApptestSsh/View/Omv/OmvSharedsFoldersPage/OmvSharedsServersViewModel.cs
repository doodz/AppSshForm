using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Services;
using SharpCifs.Smb;
using System;
using System.Threading.Tasks;

namespace ApptestSsh.Core.View.Omv.OmvSharedsFoldersPage
{
    public class OmvSharedsServersViewModel : LocalListViewModel<SharedFolder>
    {
        public OmvSharedsServersViewModel(ILogger logger) : base(logger)
        {
        }

        protected override async Task Load()
        {
            // TODO doods avoir pour utiliser cette librairie
            // using SharpCifs.Smb;
            // await ScanServers();
            //TestSharpCifs();
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

        public void TestSharpCifs()
        {
            SharpCifs.Config.SetProperty("jcifs.smb.client.lport", "8137");
            //Get local workgroups.
            var lan = new SmbFile("smb://", "");
            var workgroups = lan.ListFiles();
            foreach (var workgroup in workgroups)
            {
                Logger.Info($"Workgroup Name = {workgroup.GetName()}");

                try
                {
                    //Get servers in workgroup.
                    var servers = workgroup.ListFiles();
                    foreach (var server in servers)
                    {
                        Logger.Info($"{workgroup.GetName()} - Server Name = {server.GetName()}");

                        try
                        {
                            //Get shared folders in server. 
                            var shares = server.ListFiles();

                            foreach (var share in shares)
                                Logger.Info(
                                    $"{workgroup.GetName()}{server.GetName()} - Share Name = {share.GetName()}");
                        }
                        catch (Exception)
                        {
                            Logger.Info($"{workgroup.GetName()}{server.GetName()} - Access Denied");
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.Info($"{workgroup.GetName()} - Access Denied");
                }
            }
        }


        private async Task GetShareFolders(ISshService ssh)
        {
            var cmd = ShareMgmtService.CreateEnumerateShareCommand();
            var res = await new OmvRpcQuery<CountResultReturn<SharedFolder>>(ssh, cmd).RunAsync(Token);
            Items.ReplaceRange(res?.Data);
        }
    }
}