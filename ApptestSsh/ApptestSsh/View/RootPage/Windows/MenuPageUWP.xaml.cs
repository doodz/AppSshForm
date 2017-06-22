using Xamarin.Forms;

namespace ApptestSsh.Core.View.RootPage.Windows
{
    public partial class MenuPageUWP : ContentPage
    {
        public MenuPageUWP()
        {
            InitializeComponent();
        }

        public ListView MenuList => ListViewMenu;
    }
}