using System.Text;
using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    public class RebootQuery : GenericQuery<bool>
    {
        private string _sudoPassword;

        public RebootQuery(IClientSsh client, string sudoPassword) : base(client)
        {

            _sudoPassword = sudoPassword;

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sudoPassword))
            {
                sb.AppendFormat("echo \"{0}\" | sudo -S /sbin/shutdown -h now", _sudoPassword);
                //TODO : using halte command  
            }
        }

        protected override bool PaseResult(string result)
        {
            return true;
        }
    }
}
