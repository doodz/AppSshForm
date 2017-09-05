using Newtonsoft.Json;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Datas
{
    public class CountResultReturn<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("data")]
        public List<T> Data { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
