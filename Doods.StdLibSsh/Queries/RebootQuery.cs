using System.Text;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class RebootQuery : GenericQuery<bool>
    {
        public RebootQuery(IClientSsh client, string sudoPassword) : base(client)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sudoPassword))
            {
                sb.AppendFormat("echo \"{0}\" | sudo -S /sbin/shutdown -h now", sudoPassword);
                //TODO : using halte command  
            }
        }

        protected override bool PaseResult(string result)
        {
            return true;
        }
    }
}
