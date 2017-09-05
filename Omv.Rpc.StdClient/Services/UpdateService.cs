using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Helpers;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Services
{
    public static class UpdateService
    {
        private const string ServiceName = "Apt";

        public static OmvCommand CreateEnumerateUpgradedCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "enumerateUpgraded"
            };
            return cmd;

        }

        public static OmvCommand CreateCheckUpdateCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "update"
            };
            return cmd;

        }

        public static OmvCommand CreateUpdatePackageCommand(IEnumerable<string> packageNames)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "upgrade"
            };

            var array = JsonHelper.CreateArray("packages", packageNames);
            cmd.Params = new[] { array };
            return cmd;

        }


        public static OmvCommand CreateUpdateSettingsCommand(AptSettings aptSettings)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "setSettings"
            };

            //var obj = new JObject("params")
            //{
            //    new JProperty("partner", aptSettings.Partner),
            //    new JProperty("proposed", aptSettings.Proposed)
            //};
            cmd.Params = new[] { new JProperty("partner", aptSettings.Partner).ToString(),
                   new JProperty("proposed", aptSettings.Proposed).ToString() };
            return cmd;

        }


        public static OmvCommand CreateGetSettingsCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getSettings"
            };
            return cmd;

        }

        public static OmvCommand CreateGetChangeLogCommand(string fileName)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getChangeLog"
            };

            cmd.Params = new[] { new JProperty("filename", fileName).ToString() };
            return cmd;

        }
    }
}
