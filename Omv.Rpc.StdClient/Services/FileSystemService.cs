using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class FileSystemService
    {
        private const string ServiceName = "FileSystemMgmt";

        public static OmvCommand CreateEnumerateFileSystemCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getList"
            };


            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("start", 0));
            paramsObj.Add(new JProperty("limit", 25));
            paramsObj.Add(new JProperty("sortfield", "devicefile"));
            paramsObj.Add(new JProperty("sortdir", "ASC"));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
            return cmd;
        }

        public static OmvCommand CreateMountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "mount"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("fstab", true));
            paramsObj.Add(new JProperty("id", deviceFile));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };

            return cmd;
        }

        public static OmvCommand CreateUmountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "umount"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("fstab", true));
            paramsObj.Add(new JProperty("id", deviceFile));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };

            return cmd;
        }
    }
}