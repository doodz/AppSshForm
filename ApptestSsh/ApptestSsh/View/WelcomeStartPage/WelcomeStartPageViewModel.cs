using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.WelcomeStartPage
{
    public class WelcomeStartPageViewModel : LocalViewModel
    {
        private string _welcomMessage = "Welcome to Monitor my server";

        public ICommand GoToRootPageCmd { get; }

        public string WelcomMessage
        {
            get => _welcomMessage;
            set => SetProperty(ref _welcomMessage, value);
        }

        public WelcomeStartPageViewModel(ILogger logger) : base(logger)
        {
            GoToRootPageCmd = new Command(GoToLoginPage);
        }

        private void GoToLoginPage(object obj)
        {
            NavigationService.GoToLogin();
        }

        protected override async Task OnInternalAppearing()
        {
            if (await HaveHost())
            {
                //NavigationService.ClearHistory();
                // await NavigationService.GoToRootPage();
                App.MyApp.StartHomePage();
            }
        }

        private async Task<bool> HaveHost()
        {
            var repo = AppContainer.Container.Resolve<IRepository>();
            var list = await repo.GetAllAsync<Host>();
            if (!list.Any())
            {
                var logger = AppContainer.Container.Resolve<ILogger>();
                logger.Info($"{Title} : No host in repository.");
                return false;
            }

            var ssh = AppContainer.Container.Resolve<ISshService>();
            Host host = null;
            host = Helpers.Settings.Current.LastHostId > 0
                ? list.First(l => l.Id == Helpers.Settings.Current.LastHostId)
                : list.First();
            ssh.Host = host;
            ssh.Initialise();

            return true;
        }
    }
}