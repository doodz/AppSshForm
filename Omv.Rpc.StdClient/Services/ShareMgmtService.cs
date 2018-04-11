using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class ShareMgmtService
    {

        private const string ServiceName = "ShareMgmt";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// {"service":"ShareMgmt","method":"getList","params":{"start":0,"limit":25,"sortfield":"name","sortdir":"ASC"},"options":null}
        /// </example>
        public static OmvCommand CreateEnumerateShareCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getList"
            };

            var paramsJson = new OmvSortParamCommand(Sortfield.Name).ToJsonString();

            cmd.Params = new[]
            {
                "\""+paramsJson.Replace("\"","\\\"")+"\""
            };

            return cmd;
        }

    }
}
