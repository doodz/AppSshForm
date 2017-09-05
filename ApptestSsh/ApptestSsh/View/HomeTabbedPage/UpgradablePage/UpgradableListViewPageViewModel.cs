using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.UpgradablePage
{
    public class UpgradableListViewPageViewModel : LocalListViewModel<UpgradableBean>
    {
        public ICommand UpdateAllCmd { get; }

        public UpgradableListViewPageViewModel(ILogger logger) : base(logger)
        {
            UpdateAllCmd = new Command(UpdateAll);
        }

        private async void UpdateAll()
        {
            using (new RunWithBusyCount(this))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var res = await new NuHupQueryWithWaitPid(ssh, UpgradeAllQuery.Query).RunAsync(Token);
            }
        }

        protected override async Task RefreshData()
        {
            using (new RunWithBusyCount(this))
            {
                var ssh = AppContainer.Container.Resolve<ISshService>();
                var upgradablesBean = await new AptListQuery(ssh).RunAsync(Token);
                Items.Clear();
                Items.AddRange(upgradablesBean);
            }
        }
    }
}