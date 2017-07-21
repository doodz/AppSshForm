using Xamarin.Forms;

namespace Doods.StdFramework.Views
{
    public class DoodsButton : Button
    {
        public DoodsButton() : base()
        {
            const int animationTime = 100;
            Clicked += async (sender, e) =>
            {
                var btn = (DoodsButton)sender;
                await btn.ScaleTo(1.2, animationTime);
                await btn.ScaleTo(1, animationTime);
            };
        }
    }
}
