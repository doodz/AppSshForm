using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class NetworkSettings
    {
        [JsonProperty("hostname", Required = Required.Always)]

        public string Hostname { get; set; }
        [JsonProperty("domainname", Required = Required.Always)]
        public string Domainname { get; set; }
    }
}
