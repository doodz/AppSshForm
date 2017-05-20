using System;
using Doods.StdRepository.Interfaces;
using SQLite;

namespace Doods.StdRepository.Base
{
    public class TableBase : ITableBase
    {
        public const long DefaultId = -1;

        public DateTimeOffset SyncDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public long? Id { get; set; }

      
    }
}
