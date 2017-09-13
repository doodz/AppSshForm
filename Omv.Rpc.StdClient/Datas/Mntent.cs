using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas
{
    public class Mntent
    {
        [JsonProperty("devicefile")]
        public string Devicefile { get; set; }
        [JsonProperty("fsname")]
        public string Fsname { get; set; }
        [JsonProperty("dir")]
        public string Dir { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("posixacl")]
        public bool Posixacl { get; set; }
    }
}