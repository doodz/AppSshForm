using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdRepository.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.CommandPage
{
    public class CommandListViewPageViewModel : LocalListViewModel<CommandSsh>
    {
        private readonly IRepository _repository;
        private CommandSsh _selectedCommand;

        public CommandSsh SelectedCommand
        {
            get => _selectedCommand;
            set => SetProperty(ref _selectedCommand, value);
        }

        public ICommand AddCmdCmd { get; }

        public CommandListViewPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            _repository = reposotiry;
            AddCmdCmd = new Command(() => NavigationService.GoToAddCommandPage());
        }

        protected override async Task RefreshData()
        {
            IsBusyList = true;
            using (new RunWithBusyCount(this))
            {

                var list = await _repository.GetAllAsync<CommandSsh>();
                Items.Clear();
                Items.AddRange(list);
            }
            IsBusyList = false;
        }

        /// <summary>
        /// There is a bug.
        /// When i select an item, xamarin.forms set selecteditem two time.
        /// </summary>
        private bool _onDisplayAction;

        public async void DisplayAction()
        {
            if (SelectedCommand == null || _onDisplayAction) return;

            _onDisplayAction = true;
            var action =
                await Application.Current.MainPage.DisplayActionSheet(SelectedCommand.Name, "Cancel", null, "Run",
                    "Edite",
                    "Delete");

            switch (action)
            {
                case "Run":
                    var ssh = AppContainer.Container.Resolve<ISshService>();

                    var cmd = ssh.RunQuerry(SelectedCommand.CommandString);
                    await Application.Current.MainPage.DisplayAlert(SelectedCommand.Name, cmd.Result, "ok");
                    break;
                case "Edite":


                    await NavigationService.GoToEditCommandPage(SelectedCommand);
                    break;
                case "Delete":
                    await _repository.DeleteAsync<CommandSsh>(SelectedCommand);
                    Items.Remove(SelectedCommand);
                    SelectedCommand = null;
                    break;
                default:
                    break;
            }
            _onDisplayAction = false;
        }
    }
}
