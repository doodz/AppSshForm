using Omv.Rpc.StdClient.Commands;
using Omv.Rpc.StdClient.Helpers;
using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Services
{
    public static class PluginService
    {
        private const string ServiceName = "Plugin";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <example>
        ///
        /// </example>
        public static OmvCommand CreateEnumeratePluginsCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "enumeratePlugins"
            };
            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// {"service":"Plugin","method":"install","params":{"packages":["openmediavault-shellinabox","openmediavault-downloader"]},"options":null}
        /// </example>
        public static OmvCommand CreateInstallPluginsCommand(IEnumerable<string> pluginNames)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "install"
            };
            //TODO : doods: a revoir 
            var array = JsonHelper.CreateArray("packages", pluginNames);
            cmd.Params = new[] { array };

            return cmd;
        }
    }
}