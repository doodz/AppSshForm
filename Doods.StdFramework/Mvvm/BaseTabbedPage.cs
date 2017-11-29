using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Xamarin.Forms;

namespace Doods.StdFramework.Mvvm
{
    public class BaseTabbedPage<T> : TabbedPage where T : IViewModel
    {
        protected T ViewModel { get; }

        public BaseTabbedPage()
        {
            using (AppContainer.Container.BeginLifetimeScope())
            {
                ViewModel = AppContainer.Container.Resolve<T>();
            }
            BindingContext = ViewModel;
            Title = ViewModel.Title;



        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.OnAppearing();

            ToolbarItems.Clear();
            foreach (var toolbarItem in ViewModel.GetToolbarItems())
            {
                ToolbarItems.Add(toolbarItem);
            }
            //TODO THE : A voir pour remettre ça en place , ou alors faire une abstraction
            //Analytics.TrackEvent("OnAppearing", new Dictionary<string, string> {
            //    { "Title", Title }
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }
    }
}
