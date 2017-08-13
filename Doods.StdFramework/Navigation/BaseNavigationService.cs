using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.StdFramework.Navigation
{
    // TODO THE rendre ca exploitable ;).
    public abstract class BaseNavigationService : IBaseNavigationService
    {
        private bool _isNavigating;

        public INavigation Navigation { get; set; }

        public Page CurrentPage => Navigation.NavigationStack.FirstOrDefault();

        public Page CurrentModalPage => Navigation.ModalStack.FirstOrDefault();

        public void RemovePageFromHistory(Type type)
        {
            var last = Navigation.NavigationStack.ToList().First(p => p.GetType() == type);
            Navigation.RemovePage(last);
        }

        public void ClearHistory()
        {
            foreach (var page in Navigation.NavigationStack.ToList())
                Navigation.RemovePage(page);
        }

        protected async Task PushAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PushAsync(page, animate);
            _isNavigating = false;
        }

        protected async Task PushModalAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PushModalAsync(page, animate);
            _isNavigating = false;
        }

        protected async Task PopAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopAsync(animate);
            _isNavigating = false;
        }

        protected async Task PopModalAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopModalAsync(animate);
            _isNavigating = false;
        }

        protected async Task PopToRootAsync(INavigation navigation, bool animate = true)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            await navigation.PopToRootAsync(animate);
            _isNavigating = false;
        }


        public async Task GoBackToRoot()
        {
            await PopToRootAsync(Navigation);
        }


        public Task GoBack()
        {
            return PopAsync(Navigation);
        }
    }
}