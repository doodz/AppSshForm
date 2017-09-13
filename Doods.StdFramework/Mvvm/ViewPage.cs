using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Xamarin.Forms;

namespace Doods.StdFramework.Mvvm
{
    public class ViewPage<T> : ContentPage where T : IViewModel
    {
        protected T ViewModel { get; }

        protected ViewPage()
        {
            using (AppContainer.Container.BeginLifetimeScope())
            {
                ViewModel = AppContainer.Container.Resolve<T>();
            }
            BindingContext = ViewModel;
            Title = ViewModel.Title;


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Padding = new Thickness(0, 20, 0, 0);
                    break;
                case Device.Android:
                    Padding = new Thickness(10, 20, 0, 0);
                    break;
                case Device.WinPhone:
                case Device.UWP:
                    Padding = new Thickness(30, 20, 0, 0);
                    break;
            }
        }


        //protected void AddRefreshToolbarItem()
        //{
        //    ToolbarItems.Add(new ToolbarItem
        //    {
        //        Text = "Refresh",
        //        Icon = "Assets/ic_refresh_black_24dp_2x.png",
        //        Command = ViewModel.RefreshCommand
        //    });
        //}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.OnAppearing();
            //TODO THE : A voir pour remettre ça en place , ou alors faire une abstraction
            //Analytics.TrackEvent("OnAppearing", new Dictionary<string, string>
            //{
            //    {"Title", Title}
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }
    }
}