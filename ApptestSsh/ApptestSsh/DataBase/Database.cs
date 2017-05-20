using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
           

            await SetSchemaVersion(0);
        }

    }
}
