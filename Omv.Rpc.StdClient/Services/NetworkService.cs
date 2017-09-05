using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class NetworkService
    {
        private const string ServiceName = "Network";

        public static OmvCommand CreateGeneralSettingsCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getGeneralSettings"
            };
            return cmd;
        }
    }
}