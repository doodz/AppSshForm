using Newtonsoft.Json;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Datas.Plugins
{
    public class PluginForms
    {
        [JsonProperty("xtype")]
        public string Xtype { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("defaults")]
        public PluginFormsDefaults PluginFormsDefaults { get; set; }
        [JsonProperty("items")]
        public List<PluginFormsItem> Items { get; set; }

    }
}
