using Doods.StdFramework.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Mvvm
{
    public class ListViewPage<T> : ViewPage<T> where T : IListViewModel
    {
    //    public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    //    {
    //        ViewModel.DisplayActionItemTapped();
    //    }

    //    public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    //    {
    //        Task.FromResult(0);
    //        //TODO doods : prendre en compte la selection.
    //        //if (e.SelectedItem == null)
    //        //    return;

    //        //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

    //        ////Deselect PluginFormsItem
    //        //((ListView)sender).SelectedItem = null;
    //    }
    }
}