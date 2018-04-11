using Doods.StdFramework.Mvvm;
using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class SharedFolder : IName
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("mntentref")]
        public string Mntentref { get; set; }
        [JsonProperty("reldirpath")]
        public string Reldirpath { get; set; }
        [JsonProperty("privileges")]
        public Privileges Privileges { get; set; }
        [JsonProperty("_used")]
        public bool Used { get; set; }
        [JsonProperty("device")]
        public string Device { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("mntent")]
        public Mntent Mntent { get; set; }
    }
}