using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;
using Newtonsoft.Json;
using Omv.Rpc.StdClient.Commands;
using System;
using System.Text;

namespace Omv.Rpc.StdClient.Clients
{
    public class OmvRpcQuery<T> : GenericQuery<T>
    {
        private const string OmvRpc = "omv-rpc";
        private readonly OmvCommand _omvCommand;

        public OmvRpcQuery(IClientSsh client, OmvCommand cmd) : base(client)
        {
            _omvCommand = cmd;
            MakeCommandString();
        }

        private void MakeCommandString()
        {
            var sb = new StringBuilder();

            sb.Append(OmvRpc);
            sb.Append(" ");
            sb.Append(_omvCommand.ServiceName);
            sb.Append(" ");
            sb.Append(_omvCommand.MethodName);

            if (_omvCommand.Params != null)
            {
                foreach (var param in _omvCommand.Params)
                {
                    sb.Append(" ");
                    sb.Append(param);
                }
            }

            CmdString = sb.ToString();
        }

        protected override T PaseResult(string result)
        {
            if (result.StartsWith("ERROR", StringComparison.OrdinalIgnoreCase))
            {
                //TODO : doods : gerer les erreurs.
            }

            var res = JsonConvert.DeserializeObject<T>(result);
            return res;
        }
    }
}