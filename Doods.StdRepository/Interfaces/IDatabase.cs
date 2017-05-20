using System.Threading.Tasks;
using SQLite;

namespace Doods.StdRepository.Interfaces
{
    public interface IDatabase
    {
        Task<SQLiteAsyncConnection> GetConnection();
        Task Initialize();

        Task Reinitialize();
    }
}
