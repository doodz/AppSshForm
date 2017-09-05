using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class AptSettings
    {
        [JsonProperty("proposed")]
        public int Proposed { get; set; }
        [JsonProperty("partner")]
        public bool Partner { get; set; }
    }
}
