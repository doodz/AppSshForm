using System;
using System.Threading;
using System.Threading.Tasks;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Base.Queries
{
    public class MultiGenericQueries<T> : GenericQuery<T>
    {
        protected Func<T> Action;


        public MultiGenericQueries(IClientSsh client) : base(client)
        {
        }

        public override T Run()
        {

            if (!Client.IsConnected())
            {
                Client.Connect();
            }

            return Action();
        }

        public override async Task<T> RunAsync(CancellationToken token)
        {
            if (!Client.IsConnected())
            {
                await Client.ConnectAsync();
            }
            //return await Task.FromResult<T>(Action());
            return await Task.Run(() => Action(), token);
        }
    }
}
