using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class ExecService
    {
        private const string ServiceName = "Exec";

        public static OmvCommand CreateIsRunningCommand(string filename)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "isRunning"
            };


            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("filename", filename));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
            return cmd;
        }
    }
}