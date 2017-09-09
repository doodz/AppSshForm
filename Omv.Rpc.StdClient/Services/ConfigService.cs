using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public class ConfigService
    {
        private const string ServiceName = "Config";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static OmvCommand CreateApplyChangesBgCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "applyChangesBg"
            };

            var array = new JArray("modules");
            var obj = new JProperty("force", false);
            cmd.Params = new[] { array.ToString(), obj.ToString() };

            return cmd;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static OmvCommand CreateIsDirtyCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "isDirty"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("updatelastaccess", false));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
            return cmd;

        }

    }
}
