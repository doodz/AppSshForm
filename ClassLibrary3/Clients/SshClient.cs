using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Omv.Rpc.Client.Clients
{

    internal class OmvRpcQuery : GenericQuery<double>
    {
        public OmvRpcQuery(IClientSsh client) : base(client)
        {
        }
    }
    public class SshClient
    {

    }
}