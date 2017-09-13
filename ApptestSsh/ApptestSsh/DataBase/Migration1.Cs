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
