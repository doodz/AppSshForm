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
            return cmd;
        }

        public static OmvCommand CreateMountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "mount"
            };


            cmd.Params = new[] { new JProperty("fstab", true).ToString(), new JProperty("id", deviceFile).ToString() };

            return cmd;
        }

        public static OmvCommand CreateUmountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "umount"
            };


            cmd.Params = new[] { new JProperty("fstab", true).ToString(), new JProperty("id", deviceFile).ToString() };

            return cmd;
        }
    }
}
