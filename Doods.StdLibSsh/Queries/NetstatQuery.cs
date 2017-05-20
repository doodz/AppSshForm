using System;
using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    public class NetstatQuery : GenericQuery<String>
    {
        public NetstatQuery(IClientSsh client) : base(client)
        {
            CmdString = "netstat -a -inet";
        }
    }
}
