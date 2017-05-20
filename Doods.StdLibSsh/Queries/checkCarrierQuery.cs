using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class CheckCarrierQuery : GenericQuery<bool>
    {
        public CheckCarrierQuery(IClientSsh client,string interfaceName) : base(client)
        {
            CmdString = "cat /sys/class/net/" + interfaceName + "/carrier";
        }

        protected override bool PaseResult(string result)
        {

            if (result.Contains("1"))
            {
                //LOGGER.debug("{} has a carrier up and running.",interfaceName);
                return true;
            }
            else
            {
                //LOGGER.debug("{} has no carrier.", interfaceName);
                return false;
            }
        }
    }
}
