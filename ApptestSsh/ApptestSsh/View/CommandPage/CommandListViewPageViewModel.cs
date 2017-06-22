using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdRepository.Base;

namespace ApptestSsh.Core.View.CommandPage
{
    public class CommandListViewPageViewModel : BaseListViewModel<CommandSsh>
    {
        private readonly IRepository _repository;

        public CommandListViewPageViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            _repository = reposotiry;
        }

        protected override async Task RefreshData()
        {
            using (new RunBusy(this))
            {
                var list = await _repository.GetAllAsync<CommandSsh>();
                Items.Clear();
                Items.AddRange(list);
            }
        }
    }
}
