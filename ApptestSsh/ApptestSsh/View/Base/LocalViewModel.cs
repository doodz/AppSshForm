using ApptestSsh.Core.Services;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using System;

namespace ApptestSsh.Core.View.Base
{
    /// <summary>
    /// Viewmodel qui s’appuie sur le BaseViewModel mais permet d’exposer le service de navigation propre à l’application, 
    /// a voir pour faire un truc avec les type mais pas sûr que ce soit perf …
    /// </summary>
    public class LocalViewModel : BaseViewModel
    {
        private readonly Lazy<INavigationService> _navigationService;

        public INavigationService NavigationService => _navigationService.Value;

        public LocalViewModel(ILogger logger) : base(logger)
        {
            _navigationService =
                new Lazy<INavigationService>(() => AppContainer.Container.Resolve<INavigationService>());
        }
    }
}