using Doods.StdFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Omv.Rpc.StdClient.Datas
{
    public class SystemInfoValueSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as SystemInfoValue;
            writer.WriteStartObject();
            writer.WritePropertyName("$" + name.Text);
            serializer.Serialize(writer, name.Value);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
                return new SystemInfoValue
                {
                    Text = (string)reader.Value,
                    Value = (string)reader.Value
                };


            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();


            return new SystemInfoValue
            {
                Text = (string)properties[0].Value,
                Value = (string)properties[1].Value
            };
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

    [JsonConverter(typeof(SystemInfoValueSerializer))]
    public class SystemInfoValue : ObservableObject
    {
        private string _text;

        [JsonProperty("text")]
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _value;

        [JsonProperty("value")]
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }

    public class SystemInfoData : ObservableObject
    {
        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private SystemInfoValue _value;

        [JsonProperty("value")]
        public SystemInfoValue Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private string _type;

        [JsonProperty("type")]
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private int _index;

        [JsonProperty("index")]
        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }
    }
}