using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Services;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.MainPage
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IRepository _repository;
        public ICommand ManageHostCmd { get; }


        public MainPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            Title = "Home";
            _repository = reposotiry;
            ReloadOnAppearing = true;
            ManageHostCmd = new Command(GoToManageHost);
        }

        private void GoToManageHost()
        {
            ;
        }

        protected override async Task Load()
        {
            var list = await _repository.GetAllAsync<Host>();
            if (!list.Any())
            {
                Logger.Info($"{Title} : No host in repository");
                await NavigationService.GoToLogin();
            }
            var ssh = AppContainer.Container.Resolve<ISshService>();
            var host = list.First();
            ssh.Host = host;
            ssh.Initialise();
            await NavigationService.GoToHomeTabbed();
        }
    }
}