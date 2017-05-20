using System.Collections.Generic;
using System.Linq;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class NetworkInformationQuery : GenericQuery<IEnumerable<NetworkInterfaceInformation>>
    {
        public NetworkInformationQuery(IClientSsh client) : base(client)
        {
            CmdString = "ls -1 /sys/class/net";
        }

        private IEnumerable<NetworkInterfaceInformation> GetInterfeces(IEnumerable<string> interfaces)
        {

           var interfacesInfo = new List<NetworkInterfaceInformation>();
            // 1. find all network interfaces (excluding loopback interface) and
            // check carrier
            //var interfaces = new InterfaceQuery(Client).Run();
            //LOGGER.info("Available interfaces: {}", interfaces);

            foreach (var interfaceName in interfaces)
            {
                var interfaceInfo = new NetworkInterfaceInformation();
                interfaceInfo.Name = interfaceName;
                // check carrier
                interfaceInfo.HasCarrier = checkCarrier(interfaceName);
                interfacesInfo.Add(interfaceInfo);
            }
            var wirelessInterfaces = new List<NetworkInterfaceInformation>();
            // 2. for every interface with carrier check ip adress
            foreach (var interfaceBean in interfacesInfo)
            {
                if (interfaceBean.HasCarrier)
                {
                    interfaceBean.IpAdress = new IpAddressQuery(Client, interfaceBean.Name).Run();
                    // check if interface is wireless (interface name starts with "wlan")
                    if (interfaceBean.Name.StartsWith("wlan"))
                    {
                        // add to wireless interfaces list
                        wirelessInterfaces.Add(interfaceBean);
                    }
                }
            }
            // 3. query signal level and link status of wireless interfaces
            if (wirelessInterfaces.Any())
            {
                new WlanInfoQuery(Client, wirelessInterfaces).Run();
            }
            return interfacesInfo;
        }

        private bool checkCarrier(string interfaceName)
        {
            return new CheckCarrierQuery(Client, interfaceName).Run();
        }
        protected override IEnumerable<NetworkInterfaceInformation> PaseResult(string result)
        {
            var res = result.Split('\n').Where(r => !r.StartsWith("lo"));
            return GetInterfeces(res);
        }
    }
}
