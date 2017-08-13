using ApptestSsh.Core.Services;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using System;

namespace ApptestSsh.Core.View.Base
{
    public class LocalListViewModel<T> : BaseListViewModel<T> where T : IName
    {

        private readonly Lazy<INavigationService> _navigationService;

        public INavigationService NavigationService => _navigationService.Value;
        public LocalListViewModel(ILogger logger) : base(logger)
        {
            _navigationService =
                new Lazy<INavigationService>(() => AppContainer.Container.Resolve<INavigationService>());
        }
    }
}
