using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using System.Threading.Tasks;

namespace ApptestSsh.Core.View.UpgradablePage
{
    public class UpgradableListViewPageViewModel : BaseListViewModel<UpgradableBean>
    {

        public UpgradableListViewPageViewModel(ILogger logger) : base(logger)
        {

        }

        protected override async Task RefreshData()
        {
            using (new RunBusy(this))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var upgradablesBean = await new AptListQuery(ssh).RunAsync(Token);
                Items.Clear();
                Items.AddRange(upgradablesBean);
            }
        }
    }
}