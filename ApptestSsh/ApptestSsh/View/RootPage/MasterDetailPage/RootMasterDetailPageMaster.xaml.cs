using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.RootPage.MasterDetailPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootMasterDetailPageMaster : ViewPage<RootMasterDetailPageMasterViewModel>
    {
        public readonly ListView ListView;

        public RootMasterDetailPageMaster()
        {
            InitializeComponent();
            ListView = MenuItemsListView;
        }
    }
}