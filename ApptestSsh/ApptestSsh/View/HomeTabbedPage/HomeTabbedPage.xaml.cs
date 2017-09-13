using Doods.StdFramework.Mvvm;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : BaseTabbedPage<HomeTabbedPageViewModel>
    {
        public HomeTabbedPage()
        {
            InitializeComponent();
        }
    }
}