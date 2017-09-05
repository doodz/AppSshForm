using Newtonsoft.Json;

namespace Omv.Rpc.StdClient.Datas.Plugins
{
    public class PluginFormsDefaults
    {
        [JsonProperty("labelSeparator")]
        public string LabelSeparator { get; set; }
    }
}