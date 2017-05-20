using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Doods.StdRepository.Interfaces
{
    public  interface IMigration
    {
        int VersionNumber { get; }

        void Run(SQLiteConnection connection);
    }
}
