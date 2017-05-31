using System.Threading;
using System.Threading.Tasks;
using Doods.StdLibSsh.Interfaces;
using Renci.SshNet;

namespace Doods.StdLibSsh.Base.Queries
{
    public class GenericQuery<T>
    {

        protected readonly IClientSsh Client;
        protected string CmdString;
        protected SshCommand SshResult;
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
                Client.Connect();
            }
            SshResult = Client.Client.CreateCommand(CmdString);
           
            var str = string.Empty;
            using (SshResult)
            {
                Logger.Instance.Info($"Running command : {CmdString}.");
                str =  SshResult.Execute();
                Logger.Instance.Info($"Return Value from command : {str}.");
                //Console.WriteLine("Command>" + cmd.CommandText);
                //Console.WriteLine("Return Value = {0}", cmd.ExitStatus);
            }

            //SshResult = Client.RunQuerry(CmdString);

            
            return PaseResult(str);
            //return default(T);
            //return new T();
        }

        public virtual async Task<T> RunAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return await Task.FromCanceled<T>(token);
            }
            if (!Client.IsConnected())
            {
                await Client.ConnectAsync();
            }
            Logger.Instance.Info($"Running command async : {CmdString}.");
            var str = await Client.RunCommandAsync(CmdString,token);
            Logger.Instance.Info($"Return Value from command async: {str}.");
            return PaseResult(str);
        }

        protected virtual T PaseResult(string result)
        {
            return default(T);
        }
    }
}
