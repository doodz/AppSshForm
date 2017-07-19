using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    public class UmountQuery : GenericQuery<bool>
    {
        public UmountQuery(IClientSsh client,string device) : base(client)
        {
            CmdString = $"umount {device}";
        }

        protected override bool PaseResult(string result)
        {
            return !result.Contains("umount");
        }
    }
}
