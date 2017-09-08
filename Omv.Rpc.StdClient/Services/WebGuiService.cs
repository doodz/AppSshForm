using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    public static class WebGuiService
    {
        private const string ServiceName = "WebGui";

        public static OmvCommand CreateGetWebSettingsCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getSettings"
            };
            return cmd;

        }

    }
}
