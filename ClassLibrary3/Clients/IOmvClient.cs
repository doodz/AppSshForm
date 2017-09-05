using Omv.Rpc.Client.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omv.Rpc.Client.Clients
{
    public interface IOmvClient
    {
        //SshClient Client { get; }
        void Connect();
        Task ConnectAsync();
        bool IsConnected();
        bool CanConnect();
        bool IsAuthenticated();
        OmvCommand RunQuerry(string cmd);
        Task<string> RunCommandAsync(OmvCommand cmdStr, CancellationToken token);
    }
}
