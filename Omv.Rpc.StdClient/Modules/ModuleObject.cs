using Newtonsoft.Json;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Modules
{
    /// <summary>
    /// /usr/share/openmediavault/engined/rpc/
    /// /var/www/openmediavault/js/omv/module/admin/service/
    /// </summary>
    public class ModuleObject
    {
        [JsonProperty("xtype")]
        public string Xtype { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fieldLabel")]
        public string FieldLabel { get; set; }
        [JsonProperty("checked")]
        public bool Ischecked { get; set; }
        [JsonProperty("vtype")]
        public string Vtype { get; set; }
        [JsonProperty("minValue")]
        public int? MinValue { get; set; }
        [JsonProperty("maxValue")]
        public int? MaxValue { get; set; }
        [JsonProperty("allowDecimals")]
        public bool? AllowDecimals { get; set; }
        [JsonProperty("allowBlank")]
        public bool? AllowBlank { get; set; }
        [JsonProperty("value")]
        public int? Value { get; set; }
        [JsonProperty("boxLabel")]
        public string BoxLabel { get; set; }
        [JsonProperty("plugins")]
        public List<Plugin> Plugins { get; set; }

        [JsonProperty("enable")]
        public bool? Enable { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }
        [JsonProperty("store")]
        public Store Store { get; set; }
        [JsonProperty("displayField")]
        public string DisplayField { get; set; }
        [JsonProperty("valueField")]
        public string ValueField { get; set; }
        [JsonProperty("editable")]
        public bool? Editable { get; set; }
        [JsonProperty("triggerAction")]
        public string TriggerAction { get; set; }
        [JsonProperty("value")]
        public int? value { get; set; }
        [JsonProperty("border")]
        public bool? Border { get; set; }
        [JsonProperty("html")]
        public string Html { get; set; }
    }

    public class Plugin
    {
        [JsonProperty("ptype")]
        public string Ptype { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Store
    {
        [JsonProperty("fields")]
        public List<string> Fields { get; set; }
        [JsonProperty("data")]
        public List<List<object>> Data { get; set; }
    }
    public class Defaults
    {
        [JsonProperty("labelSeparator")]
        public string LabelSeparator { get; set; }
    }
}
