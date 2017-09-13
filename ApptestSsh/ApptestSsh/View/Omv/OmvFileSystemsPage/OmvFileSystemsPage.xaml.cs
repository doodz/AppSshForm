using Doods.StdFramework.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvFileSystemsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OmvFileSystemsPage : ListViewPage<OmvFileSystemsPageViewModel>
    {
        public OmvFileSystemsPage()
        {
            InitializeComponent();
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.DisplayActionItemTapped();
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Task.FromResult(0);
            //TODO doods : prendre en compte la selection.
            //if (e.SelectedItem == null)
            //    return;

            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            ////Deselect PluginFormsItem
            //((ListView)sender).SelectedItem = null;
        }
    }
}