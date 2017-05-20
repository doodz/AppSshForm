using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdRepository.Base;

namespace ApptestSsh.Core.DataBase
{
    public class Host : TableBase
    {
        public int Port { get; set; }

        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
