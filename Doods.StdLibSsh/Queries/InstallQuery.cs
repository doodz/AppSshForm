using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class InstallQuery : GenericQuery<bool>
    {
        public InstallQuery(IClientSsh client, string packageName) : base(client)
        {
            CmdString = $"sudo apt-get install {packageName}";
        }
    }
}
