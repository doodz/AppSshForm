using System;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class NetstatQuery : GenericQuery<String>
    {
        public NetstatQuery(IClientSsh client) : base(client)
        {
            CmdString = "netstat -a -inet";
        }
    }
}
