using Doods.StdRepository.Interfaces;
using SQLite;

namespace ApptestSsh.Core.DataBase
{
    class Migration2 : IMigration
    {
        public int VersionNumber => 2;

        public void Run(SQLiteConnection connection)
        {

            //connection.CreateCommand()

            //ALTER TABLE { tableName}
            //ADD COLUMN COLNew { type};
        }
    }
}