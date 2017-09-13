using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class ShareMgmtService
    {

        private const string ServiceName = "ShareMgmt";
        public static OmvCommand CreateEnumerateShareCommand()
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
