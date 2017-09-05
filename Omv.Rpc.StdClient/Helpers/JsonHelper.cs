using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Helpers
{
    public static class JsonHelper
    {
        public static string CreateArray(string ArrayName, IEnumerable<string> items)
        {
            var array = new JArray();

            foreach (var pluginName in items)
                array.Add(pluginName);
            var o = new JObject { [ArrayName] = array };

            return o.ToString();
        }
    }
}