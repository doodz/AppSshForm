using Doods.StdFramework.Mvvm;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.WelcomeStartPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeStartPage : ViewPage<WelcomeStartPageViewModel>
    {
        public WelcomeStartPage()
        {
            InitializeComponent();
        }
    }
}