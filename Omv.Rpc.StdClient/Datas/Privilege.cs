using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class Privilege
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("perms")]
        public int Perms { get; set; }
    }
}