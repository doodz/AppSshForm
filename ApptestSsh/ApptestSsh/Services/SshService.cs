using ApptestSsh.Core.DataBase;
using Doods.LibSsh;
using Renci.SshNet;
namespace ApptestSsh.Core.Services
{
    public class SshService : SshServiceBase , ISshService
    {
        private Host _host;

        public Host Host
        {
            get { return _host; }
            set { _host = value;Initialise(); }
        }
        

        public void Initialise()
        {
            HostName = _host.HostName;
            UserName = _host.UserName;
            Password = _host.Password;
            Dispose();
        }

        
    }
}
