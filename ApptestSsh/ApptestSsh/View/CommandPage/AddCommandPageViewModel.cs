using System.Windows.Input;
using ApptestSsh.Core.DataBase;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.CommandPage
{
    public class AddCommandPageViewModel : BaseViewModel
    {
        private readonly IRepository _repository;
        public string CmdName { get; set; }
        public string CmdString { get; set; }
        public ICommand SaveCmd { get;}
        public AddCommandPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            _repository = reposotiry;
            SaveCmd = new Command(Save);
        }

        private async void Save(object obj)
        {
            var comm = new CommandSsh();

            if(comm.Id ==null)
                await _repository.InsertAsync(comm);
            else
                await _repository.UpdateAsync(comm);
        }
    }
}
