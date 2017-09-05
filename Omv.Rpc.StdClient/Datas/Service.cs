using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class Service
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }
        [JsonProperty("enabled", Required = Required.Always)]
        public bool Enabled { get; set; }
        [JsonProperty("running", Required = Required.Always)]
        public bool Running { get; set; }
    }
}
