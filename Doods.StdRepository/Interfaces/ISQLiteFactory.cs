using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doods.StdRepository.Interfaces
{
    public interface ISQLiteFactory
    {
        string GetDatabasePath(string fileName);
    }
}
