using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class DiskMgmtService
    {

        private const string ServiceName = "DiskMgmt";


        public static OmvCommand CreateEnumerateDiskCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getList"
            };
            return cmd;
        }

    }
}
