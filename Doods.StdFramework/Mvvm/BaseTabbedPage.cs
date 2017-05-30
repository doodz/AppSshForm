using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }
    }
}
