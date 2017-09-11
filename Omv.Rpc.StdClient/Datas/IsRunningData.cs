using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class IsRunningData
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }
        [JsonProperty("running")]
        public bool Running { get; set; }
    }
}
