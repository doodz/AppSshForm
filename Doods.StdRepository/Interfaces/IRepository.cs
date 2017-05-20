using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doods.StdRepository.Base
{
    public interface IRepository
    {
        Task<List<T>> GetAllAsync<T>() where T : TableBase, new();

        Task<List<T>> GetAllWithChildrenAsync<T>() where T : TableBase, new();

        Task<T> FindAsync<T>(long? id) where T : TableBase, new();
        Task<T> FindWithChildrenAsync<T>(long? id) where T : TableBase, new();

        Task InsertAsync<T>(T value) where T : TableBase, new();
        Task InsertWithChildrenAsync<T>(T value) where T : TableBase, new();

        Task UpdateAsync<T>(T value) where T : TableBase, new();
        Task UpdateWithChildrenAsync<T>(T value) where T : TableBase, new();

        Task DeleteAsync<T>(T value) where T : TableBase, new();
        Task DeleteWithChildrenAsync<T>(T value) where T : TableBase, new();
    }
}