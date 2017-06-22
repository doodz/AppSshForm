using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.StdRepository.Interfaces;
using mDatabase= Doods.StdRepository.Base.Database;
namespace ApptestSsh.Core.DataBase
{
    class Database : mDatabase
    {
       

        public Database(ISQLiteFactory factory):base(factory, "DoodsSshApp.db")
        {
        }

        protected override IEnumerable<IMigration> Migrations
        {
            get { yield return new Migration1(); }
        }

       

        public override async Task Initialize()
        {
            try
            {
                await AsyncConnection.CreateTableAsync<Host>();
                await AsyncConnection.CreateTableAsync<Command>();

                await Migrate();

                Tcs.TrySetResult(null);
            }
            catch (Exception e)
            {
                Tcs.TrySetException(e);
                throw;
            }
        }
        public override async Task Drop()
        {
            await AsyncConnection.DropTableAsync<Host>();
            await AsyncConnection.DropTableAsync<Command>();


            await SetSchemaVersion(0);
        }

    }
}
