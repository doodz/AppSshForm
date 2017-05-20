using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdRepository.Interfaces;
using SQLite;

namespace Doods.StdRepository.Base
{
    public abstract class Database : IDatabase
    {
        protected readonly SQLiteAsyncConnection AsyncConnection;
        protected TaskCompletionSource<object> Tcs = new TaskCompletionSource<object>();

        protected Database(ISQLiteFactory factory, string dbname)
        {
            AsyncConnection = new SQLiteAsyncConnection(factory.GetDatabasePath(dbname));
        }

        protected abstract IEnumerable<IMigration> Migrations { get; }

        public async Task<SQLiteAsyncConnection> GetConnection()
        {
            await Tcs.Task;
            return AsyncConnection;
        }



        public async Task Reinitialize()
        {
            Tcs = new TaskCompletionSource<object>();

            await Drop();
            await Initialize();
        }

        public virtual async Task Initialize()
        {
            

        }

        public virtual async Task Drop()
        {

            await SetSchemaVersion(0);
        }

        protected async Task Migrate()
        {
            var currentVersion = await GetSchemaVersion();
            var migrations = Migrations.Where(m => m.VersionNumber > currentVersion).ToList();

            if (migrations != null && migrations.Any())
                foreach (var migration in migrations)
                {
                    await AsyncConnection.RunInTransactionAsync(c => migration.Run(c));
                    await SetSchemaVersion(migration.VersionNumber);
                }
        }

        private Task<int> GetSchemaVersion()
        {
            return AsyncConnection.ExecuteScalarAsync<int>("PRAGMA user_version;");
        }

        protected Task SetSchemaVersion(int version)
        {
            return AsyncConnection.ExecuteAsync($"PRAGMA user_version = {version};");
        }

    }
}
