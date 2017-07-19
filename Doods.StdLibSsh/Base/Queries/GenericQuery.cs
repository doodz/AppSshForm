using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh.Interfaces;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Doods.StdLibSsh.Base.Queries
{
    public class GenericQuery<T>
    {
        protected readonly IClientSsh Client;
        protected string CmdString;
        protected SshCommand Sshcmd;

        public GenericQuery(IClientSsh client)
        {
            Client = client;
        }

        protected IClientSsh GetClient()
        {
            return Client;
        }

        public virtual T Run()
        {
            if (!Client.IsConnected())
            {
                Logger.Instance.Info($"Client not connected, Connect.");
                Client.Connect();
            }
            Sshcmd = Client.Client.CreateCommand(CmdString);


            string str;
            
            using (Sshcmd)
            {
                Logger.Instance.Info($"Running command : {CmdString}.");
                str = Sshcmd.Execute();
                Logger.Instance.Info($"Return Value from command : {str}.");
                

            }
            
            var result = PaseResult(str);
            //TODO Doods : QueryResult
            var objres = new QueryResult<T>()
            {
                Query = CmdString,
                BashLines = str,
                Result = result,
                ExitStatus = Sshcmd.ExitStatus,
                Error = Sshcmd.Error
            };
            return result;
        }

        public virtual async Task<T> RunAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return await Task.FromCanceled<T>(token);
            }
            if (!Client.IsConnected())
            {
                Logger.Instance.Info("Client not connected, ConnectAsync.");
                await Client.ConnectAsync();
            }
            Logger.Instance.Info($"Running command async : {CmdString}.");

            //TODO Doods : SshConnectionException
            var cmd = Client.Client.CreateCommand(CmdString);
            var str = await Client.RunCommandAsync(cmd, token);
            Logger.Instance.Info($"Return Value from command async: {str}.");
            var result = await PaseResultAsync(str, token);

            //TODO Doods : QueryResult
            var objres = new QueryResult<T>
            {
                Query = CmdString,
                Result = result,
                BashLines = str,
                ExitStatus = cmd.ExitStatus,
                Error = cmd.Error
            };

            return result;
        }

        private async Task<T> PaseResultAsync(string result, CancellationToken token)
        {
            using (CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                var res = await Task.Factory.StartNew<T>(() => PaseResult(result), token);
                return res;
            }
        }

        protected virtual T PaseResult(string result)
        {
            return default(T);
        }
    }
}