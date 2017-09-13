using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Interfaces
{
    public interface IViewModel
    {
        string Title { get; set; }
        string Subtitle { get; set; }
        string Icon { get; set; }
        Task OnAppearing();
        Task OnDisappearing();
        IEnumerable<ToolbarItem> GetToolbarItems();
    }
}
