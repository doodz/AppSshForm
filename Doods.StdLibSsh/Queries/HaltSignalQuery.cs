using System.Text;
using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    public class HaltSignalQuery : GenericQuery<bool>
    {
        private string _sudoPassword;

        public HaltSignalQuery(IClientSsh client, string sudoPassword) : base(client)
        {
            _sudoPassword = sudoPassword;

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sudoPassword))
            {
                sb.AppendFormat("echo \"{0}\" | sudo -S /sbin/shutdown -r now", _sudoPassword);
                //TODO : using halte command  
            }
        }

        protected override bool PaseResult(string result)
        {
            return true;
        }
    }
}
