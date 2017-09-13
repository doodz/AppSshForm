using System.Threading.Tasks;

namespace Doods.StdFramework.Interfaces
{
    public interface IListViewModel : IViewModel
    {
        Task DisplayActionItemTapped();
    }
}