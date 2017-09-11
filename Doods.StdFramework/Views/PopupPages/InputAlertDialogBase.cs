using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Views.PopupPages
{
    public class InputAlertDialogBase<T> : PopupPage
    {

        /// <summary>
        /// The awaitable task
        /// </summary>
        public Task<T> PageClosedTask => PageClosedTaskCompletionSource.Task;


        /// <summary>
        /// The task completion source
        /// </summary>
        public TaskCompletionSource<T> PageClosedTaskCompletionSource { get; set; }

        public InputAlertDialogBase(View contentBody)
        {
            // init the task completion source
            PageClosedTaskCompletionSource = new TaskCompletionSource<T>();
            Content = contentBody;

            BackgroundColor = new Color(0, 0, 0, 0.4);
        }

        /// <summary>
        /// Method for animation child in PopupPage
        /// Invoced after custom animation end
        /// </summary>
        /// <returns></returns>
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);
        }

        /// <summary>
        /// Method for animation child in PopupPage
        /// Invoked before custom animation begin
        /// </summary>
        /// <returns></returns>
        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent back button pressed action on android
            //return base.OnBackButtonPressed();
            return true;
        }

        /// <summary>
        /// Invoced when background is clicked
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackgroundClicked()
        {
            // Prevent background clicked action
            //return base.OnBackgroundClicked();
            return false;
        }
    }
}