using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;

namespace Doods.StdLibSsh.Interfaces
{
    public interface IClientSsh
    {
        SshClient Client { get; }
        void Connect();
        Task ConnectAsync();
        bool IsConnected();
        bool CanConnect();
        bool IsAuthenticated();
        SshCommand RunQuerry(string cmd);
        Task<string> RunCommandAsync(string cmdStr, CancellationToken token);
    }
}
