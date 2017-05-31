using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class CheckCarrierQuery : GenericQuery<bool>
    {
        private readonly string _interfaceName;
        public CheckCarrierQuery(IClientSsh client,string interfaceName) : base(client)
        {
            _interfaceName = interfaceName;
            CmdString = "cat /sys/class/net/" + interfaceName + "/carrier";
        }

        protected override bool PaseResult(string result)
        {

            if (result.Contains("1"))
            {
                Logger.Instance.Debug($"{_interfaceName} has a carrier up and running.");
                return true;
            }
            else
            {
                Logger.Instance.Debug($"{_interfaceName} has no carrier.");
                return false;
            }
        }
    }
}
