using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.HostManagerPage
{
    public class HostManagerPageViewModel : LocalViewModel
    {

        public ICommand AddHostCmd { get; }
        private readonly IRepository _repository;
        public ObservableRangeCollection<Host> Items { get; }

        private Host _selectedHost;

        public Host SelectedHost
        {
            get => _selectedHost;
            set => SetProperty(ref _selectedHost, value);
        }

        /// <summary>
        /// There is a bug.
        /// When i select an item, xamarin.forms set selecteditem two time.
        /// </summary>
        private bool _onDisplayAction;

        public async void DisplayAction()
        {
            if (SelectedHost == null || _onDisplayAction) return;

            _onDisplayAction = true;
            var action =
                await Application.Current.MainPage.DisplayActionSheet(SelectedHost.HostName, "Cancel", null, "Select",
                    "Edite",
                    "Delete");

            switch (action)
            {
                case "Select":
                    var ssh = AppContainer.Container.Resolve<ISshService>();
                    ssh.Host = SelectedHost;
                    ssh.Initialise();
                    await NavigationService.GoBack();
                    break;
                case "Edite":
                    await NavigationService.GoToLogin(SelectedHost);
                    break;
                case "Delete":
                    await _repository.DeleteAsync<Host>(SelectedHost);


                    if (Helpers.Settings.Current.LastHostId == SelectedHost.Id)
                        Helpers.Settings.Current.LastHostId = -1;

                    Items.Remove(SelectedHost);
                    SelectedHost = null;
                    break;
                default:
                    break;
            }
            _onDisplayAction = false;
        }

        public HostManagerPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            Items = new ObservableRangeCollection<Host>();

            _repository = reposotiry;

            RefreshDataCommand = new Command(
                async () => await RefreshData());

            AddHostCmd = new Command(() => NavigationService.GoToLogin());
        }

        protected override async Task Load()
        {
            await RefreshData();
        }

        private bool _isBusyList;

        public bool IsBusyList
        {
            get => _isBusyList;
            set => SetProperty(ref _isBusyList, value);
        }

        public ICommand RefreshDataCommand { get; }

        private async Task RefreshData()
        {
            BusyCount++;
            IsBusyList = true;
            //Load Data Here
            //await Task.Delay(2000);
            var list = await _repository.GetAllAsync<Host>();

            Items.Clear();
            Items.AddRange(list);
            BusyCount--;
            IsBusyList = false;
            ((Command)RefreshDataCommand).ChangeCanExecute();
        }
    }
}