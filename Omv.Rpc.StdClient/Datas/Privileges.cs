using System.Collections.Generic;
using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class Privileges
    {
        [JsonProperty("privilege")]
        public List<Privilege> Privilege { get; set; }
    }
}