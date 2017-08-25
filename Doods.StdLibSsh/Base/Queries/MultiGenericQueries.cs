using Doods.StdLibSsh.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            if (token.IsCancellationRequested)
            {
                return await Task.FromResult<T>(default(T));
                //return await Task.FromCanceled<T>(token);
            }

            if (!Client.IsConnected())
            {
                await Client.ConnectAsync();
            }
            return await Task.FromResult<T>(Action());

        }
    }
}
