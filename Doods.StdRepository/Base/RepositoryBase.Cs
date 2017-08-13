using Doods.StdRepository.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//using SQLiteNetExtensionsAsync.Extensions;

namespace Doods.StdRepository.Base
{
    public class RepositoryBase : IRepository
    {
        private readonly IDatabase _database;

        public RepositoryBase(IDatabase database)
        {
            _database = database;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : TableBase, new()
        {
            var cnt = await _database.GetConnection();
            return await cnt.Table<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllWithChildrenAsync<T>() where T : TableBase, new()
        {
            var cnt = await _database.GetConnection();
            return new List<T>();
            //Le package SQLiteNetExtensions.Async 2.0.0 - alpha2 n'est pas compatible avec netstandard1.3 (.NETStandard,Version=v1.3).
            //return await cnt.GetAllWithChildrenAsync<T>();
        }

        public async Task<T> FindAsync<T>(long? id) where T : TableBase, new()
        {
            if (id == null) return default(T);

            var cnt = await _database.GetConnection();
            return await cnt.Table<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> FindWithChildrenAsync<T>(long? id) where T : TableBase, new()
        {
            if (id == null) return default(T);

            var cnt = await _database.GetConnection();
            return new T();
            //Le package SQLiteNetExtensions.Async 2.0.0-alpha2 n'est pas compatible avec netstandard1.3 (.NETStandard,Version=v1.3).
            //return await cnt.FindWithChildrenAsync<T>(id);
        }

        public async Task InsertAsync<T>(T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection();
            await cnt.InsertOrReplaceAsync(value);
        }

        public Task InsertWithChildrenAsync<T>(T value) where T : TableBase, new()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync<T>(T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection();
            await cnt.UpdateAsync(value);
        }

        public Task UpdateWithChildrenAsync<T>(T value) where T : TableBase, new()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync<T>(T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection();
            await cnt.DeleteAsync(value);
        }

        public Task DeleteWithChildrenAsync<T>(T value) where T : TableBase, new()
        {
            throw new NotImplementedException();
        }

        protected Task<SQLiteAsyncConnection> GetConnection()
        {
            return _database.GetConnection();
        }
    }
}