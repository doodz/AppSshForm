using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Base.Queries
{
    public class AllYesQuery : GenericQuery<bool>
    {
        public AllYesQuery(IClientSsh client, string command) : base(client)
        {
            CmdString = $"yes \"\" | {command}";
        }
    }
}
