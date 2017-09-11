using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class ConfigService
    {
        private const string ServiceName = "Config";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// {"service":"Config","method":"applyChangesBg","params":{"modules":[],"force":false},"options":null}
        /// </example>
        public static OmvCommand CreateApplyChangesBgCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "applyChangesBg"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("modules", new JArray()));
            paramsObj.Add(new JProperty("force", false));

            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
            return cmd;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// {"service":"Config","method":"revertChangesBg","params":{"filename":""},"options":null}
        /// </example>
        public static OmvCommand CreateRevertChangesBgCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "revertChangesBg"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("filename", ""));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
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
