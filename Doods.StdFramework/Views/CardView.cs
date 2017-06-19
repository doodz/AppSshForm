using Xamarin.Forms;

namespace Doods.StdFramework.Views
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 0;
           
            if (Device.iOS == Device.RuntimePlatform)
            {
                HasShadow = false;
                OutlineColor = Color.Transparent;
                BackgroundColor = Color.Transparent;
            }
        }
    }
}