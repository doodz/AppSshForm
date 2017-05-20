using System.Collections.Generic;
using System.Linq;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  ls -1 /sys/class/net
    ///  eth0
    /// lo
    /// tun0
    /// </example>
    public class InterfaceQuery : GenericQuery<IEnumerable<string>>
    {
        public InterfaceQuery(IClientSsh client) : base(client)
        {
            CmdString = "ls -1 /sys/class/net";
        }

        protected override IEnumerable<string> PaseResult(string result)
        {
           
            return result.Split('\n').Where(r=> !r.StartsWith("lo"));
        }
    }
}
