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
                str =  SshResult.Execute();
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
            var str = await Client.RunCommandAsync(CmdString,token);
            return PaseResult(str);
        }

        protected virtual T PaseResult(string result)
        {
            return default(T);
        }
    }
}
