using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class SystemService
    {
        private const string ServiceName = "System";

        public static OmvCommand CreateSystemInformationCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getInformation"
            };
            return cmd;

        }

        public static OmvCommand CreateStatusServicesCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = "Services",
                MethodName = "getStatus"
            };
            return cmd;

        }
    }
}