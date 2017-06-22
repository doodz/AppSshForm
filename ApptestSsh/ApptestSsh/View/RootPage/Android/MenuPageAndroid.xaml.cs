using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.RootPage.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPageAndroid : ContentPage
    {
        private readonly RootPageAndroid _root;
        public MenuPageAndroid(RootPageAndroid root)
        {
            _root = root;
            InitializeComponent();

            NavView.NavigationItemSelected += async (sender, e) =>
            {
                _root.IsPresented = false;

                await Task.Delay(225);
                await _root.NavigateAsync(e.Index);
            };
        }
    }  
}