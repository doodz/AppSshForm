using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Omv.Rpc.StdClient.Ssh.Queries
{
    public class GenerateRrdQuery : GenericQuery<bool>
    {
        public static readonly string Query = "/usr/sbin/omv-mkgraph";
        public GenerateRrdQuery(IClientSsh client) : base(client)
        {
            CmdString = Query;
        }

        protected override bool PaseResult(string result)
        {
            return true;
        }
    }
}
