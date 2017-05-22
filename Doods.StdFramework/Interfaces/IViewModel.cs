using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doods.StdFramework.Interfaces
{
    public interface IViewModel
    {
        string Title { get; set; }
        string Subtitle { get; set; }
        string Icon { get; set; }
        Task OnAppearing();
        Task OnDisappearing();
    }
}
