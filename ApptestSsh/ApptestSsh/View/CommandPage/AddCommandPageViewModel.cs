using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.View.Base;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.CommandPage
{
    public class AddCommandPageViewModel : LocalViewModel
    {
        private readonly IRepository _repository;
        public string CmdName { get; set; }
        public string CmdString { get; set; }
        public ICommand SaveCmd { get; }
        private CommandSsh _commandSsh;

        public CommandSsh CommandSsh
        {
            get => _commandSsh;
            set
            {
                SetProperty(ref _commandSsh, value);
                if (value != null)
                {
                    CmdString = value.CommandString;
                    CmdName = value.Name;
                }
            }
        }

        public AddCommandPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            _repository = reposotiry;
            SaveCmd = new Command(Save);
            CommandSsh = new CommandSsh();
        }

        private async void Save(object obj)
        {
            CommandSsh.Name = CmdName;
            CommandSsh.CommandString = CmdString;
            if (CommandSsh.Id == null)
                await _repository.InsertAsync(CommandSsh);
            else
                await _repository.UpdateAsync(CommandSsh);
            await NavigationService.GoBack();
        }
    }
}