using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Navigation
{
    public interface IBaseNavigationService
    {
        INavigation Navigation { get; set; }
        Page CurrentPage { get; }
        Page CurrentModalPage { get; }

        void ClearHistory();
        Task GoBackToRoot();

        Task GoBack();
    }
}