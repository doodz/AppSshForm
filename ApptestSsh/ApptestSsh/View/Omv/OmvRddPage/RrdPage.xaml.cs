using Doods.StdFramework.Mvvm;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Omv.OmvRddPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RrdPage : ViewPage<RrdPageViewModel>
    {
        public RrdPage()
        {
            InitializeComponent();
        }
    }
}