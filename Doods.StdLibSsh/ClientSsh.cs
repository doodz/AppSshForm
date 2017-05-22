using System.Threading;
using System.Threading.Tasks;
using Doods.StdLibSsh.Interfaces;
using Renci.SshNet;

namespace Doods.StdLibSsh
{
    public class ClientSsh : IClientSsh
    {
        public  SshClient Client { get; private set; }

        public void Connect()
        {
            Client = new SshClient("192.168.1.73", "pi", "raspberry");
            Client.Connect();

        }

       

        public string GetServeurVersion()
        {

            using (var client = new SshClient("192.168.1.50", "root", "nostrqdq;us"))
            {
                client.Connect();
                
                return client.ConnectionInfo.ServerVersion;
              
            }
        }

        public bool IsAuthenticated()
        {
            if (Client == null) return false;
            return Client.IsConnected;
        }

        public bool IsConnected()
        {
            return IsAuthenticated();
           
        }


        public SshCommand RunQuerry(string cmd)
        {
            return Client.RunCommand(cmd);
        }

        public Task<string> RunCommandAsync(string cmdStr, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
        public Task ConnectAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
