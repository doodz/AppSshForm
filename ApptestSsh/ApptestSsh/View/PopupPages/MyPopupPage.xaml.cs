using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopupPage : PopupPage
    {
        public MyPopupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }
    }

}

//private async void OnOpenPupup(object sender, EventArgs e)
//{
//var page = new MyPopupPage();

//await Navigation.PushPopupAsync(page);
//// or
//await PopupNavigation.PushAsync(page);
//}

