using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdRepository.Interfaces;
using SQLite;

namespace ApptestSsh.Core.DataBase
{
    class Migration1 : IMigration
    {
        public int VersionNumber => 1;

        public void Run(SQLiteConnection connection)
        {
           
        }
    }
}
