using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Omv.Rpc.StdClient.Commands
{
    public class OmvCommand
    {

        public string Options { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public IEnumerable<string> Params { get; set; }

    }

    public class OmvSortParamCommand
    {
        [JsonProperty("start")]
        public int Start { get; set; } = 0;

        [JsonProperty("limit")]
        public int Limit { get; set; } = 25;
        [JsonProperty("sortfield")]
        public Sortfield Sortfield { get; set; }

        [JsonProperty("sortdir")]
        public Sortdir Sortdir { get; set; } = Sortdir.Asc;


        public OmvSortParamCommand(Sortfield sortfield)
        {
            Sortfield = sortfield;
        }


        public string ToJsonString()
        {

            var res = $"\"start\":{Start},\"limit\":{Limit},\"sortfield\":\"{Sortfield.GetAttribute<EnumMemberAttribute>().Value}\",\"sortdir\":\"{Sortdir.GetAttribute<EnumMemberAttribute>().Value}\"";
            return "{" + res + "}";
        }
    }

    public enum Sortdir
    {
        [EnumMember(Value = "ASC")]

        Asc,

        [EnumMember(Value = "DESC")]

        Desc
    }
    public enum Sortfield
    {
        [EnumMember(Value = "name")]

        Name,

        [EnumMember(Value = "devicefile")]

        Devicefile
    }



    public static class Extensions
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }


       
    }
}
