using Xamarin.Forms;

namespace Doods.StdFramework.Navigation
{
    public class DoodsNavigationPage : NavigationPage
    {
        public DoodsNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public DoodsNavigationPage()
        {
            Init();
        }

        void Init()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                BarBackgroundColor = Color.FromHex("FAFAFA");
            }
            else
            {
                BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
                BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            }
        }
    }
}