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
    public class ViewPage<T> : ContentPage where T: IViewModel
    {
        readonly T _viewModel;
        public T ViewModel { get { return _viewModel; } }

        public ViewPage()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _viewModel = AppContainer.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
        }
    }
}