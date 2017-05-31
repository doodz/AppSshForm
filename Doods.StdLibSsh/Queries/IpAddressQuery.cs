using System.Text.RegularExpressions;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  ip -f inet addr show dev eth0 | sed -n 2p
    ///  inet 192.168.1.73/24 brd 192.168.1.255 scope global eth0
    /// </example>
    public class IpAddressQuery : GenericQuery<string>
    {
        private Regex IPADDRESS_PATTERN =
                new Regex(
                    "\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b")
            ;

        private string _name;

        public IpAddressQuery(IClientSsh client, string name) : base(client)
        {
            CmdString = "ip -f inet addr show dev " + name + " | sed -n 2p";
            _name = name;
        }

        protected override string PaseResult(string result)
        {

            //LOGGER.debug("Output of ip query: {}", output);
            var match = IPADDRESS_PATTERN.Match(result);

            if (match.Success)
            {
                var ipAddress = match.Value.Trim();
                //string ipAddress = match.group().trim();
                //LOGGER.info("{} - IP address: {}.", name, ipAddress);
                return ipAddress;
            }
            else
            {
                Logger.Instance.Error($"IP address pattern: No match found for output: {result}.");
            }
            Logger.Instance.Info($"'ip' command not available. Trying '/sbin/ifconfig' to get ip address of interface {_name}.");

            return new IfConfigQuery(Client, _name).Run();
        }
    }
}
