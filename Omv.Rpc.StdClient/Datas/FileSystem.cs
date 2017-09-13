using Newtonsoft.Json;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Datas
{
    public enum FileSystemStatus
    {
        Online = 1,
        Initializing = 2,
        Missing = 3
    }
    public class FileSystem
    {
        [JsonProperty("devicefile")]
        public string Devicefile { get; set; }

        [JsonProperty("devicefiles")]
        public List<string> Devicefiles { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("blocks")]
        public string Blocks { get; set; }

        [JsonProperty("mounted")]
        public bool Mounted { get; set; }

        [JsonProperty("mountable")]
        public bool Mountable { get; set; }

        [JsonProperty("mountpoint")]
        public string Mountpoint { get; set; }

        [JsonProperty("used")]
        public string UsedSize { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("percentage")]
        public int Percentage { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("propposixacl")]
        public bool Propposixacl { get; set; }

        [JsonProperty("propquota")]
        public bool Propquota { get; set; }

        [JsonProperty("propresize")]
        public bool Propresize { get; set; }

        [JsonProperty("propfstab")]
        public bool Propfstab { get; set; }

        [JsonProperty("propcompress")]
        public bool Propcompress { get; set; }

        [JsonProperty("propautodefrag")]
        public bool Propautodefrag { get; set; }

        [JsonProperty("hasMultipleDevices")]
        public bool HasMultipleDevices { get; set; }

        [JsonProperty("status")]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public FileSystemStatus Status { get; set; }
        public int Status { get; set; }
        [JsonProperty("_used")]
        public bool IsReferenced { get; set; }

        [JsonProperty("parentdevicefile")]
        public string Parentdevicefile { get; set; }

        [JsonProperty("propreadonly")]
        public bool? Propreadonly { get; set; }

        [JsonProperty("hasmultipledevices")]
        public bool? Hasmultipledevices { get; set; }

        [JsonProperty("_readonly")]
        public bool? IsReadonly { get; set; }


        [JsonIgnore]
        public string LabelDescription
        {
            get
            {
                var str = Label;
                if (string.IsNullOrEmpty(Label))
                    str = Devicefile;

                return $"{str} - {(FileSystemStatus)Status}  referenced: {IsReferenced} mounted: {Mounted}";
            }
        }
    }
}