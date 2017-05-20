using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Doods.LibSsh.Interfaces;
using Doods.LibSsh.Queries;

namespace Doods.StdLibSsh.Base.Queries
{
    public class MultiGenericQueries<T> : GenericQuery<T>
    {
        protected Func<T> Action;


        public MultiGenericQueries(IClientSsh client) : base(client)
        {
        }

        public T Run()
        {

            if (!Client.IsConnected())
            {
                Client.Connect();
            }

            return Action();
        }

        public async Task<T> RunAsync(CancellationToken token)
        {
            if (!Client.IsConnected())
            {
                Client.Connect();
            }
            //return await Task.FromResult<T>(Action());
            return await Task.Run(() => Action(), token);
        }
    }
}
