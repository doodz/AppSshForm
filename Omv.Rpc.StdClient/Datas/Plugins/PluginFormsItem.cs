using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Omv.Rpc.StdClient.Datas.Plugins
{
    public class PluginFormsItem
    {
        [JsonProperty("xtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PluginFormsItemType Xtype { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("boxLabel")]
        public string BoxLabel { get; set; }
        [JsonProperty("fieldLabel")]
        public string FieldLabel { get; set; }
        [JsonProperty("checked")]
        public bool Checked { get; set; }
        [JsonProperty("allowBlank")]
        public bool? AllowBlank { get; set; }
    }
}