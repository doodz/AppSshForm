using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using System.Linq;

namespace ApptestSsh.Core.View.RootPage.MasterDetailPage
{
    public class RootMasterDetailPageMasterViewModel : BaseViewModel
    {
        public ObservableRangeCollection<RootMasterDetailPageMenuItem> MenuItems { get; set; }

        public RootMasterDetailPageMenuItem Item { get; set; }
        public RootMasterDetailPageMasterViewModel(ILogger logger) : base(logger)
        {
            var i = 0;
            MenuItems = new ObservableRangeCollection<RootMasterDetailPageMenuItem>(new[]
            {
                new RootMasterDetailPageMenuItem {Id = i++, Title = "Home",TargetType = typeof(HomeTabbedPage.HomeTabbedPage)},
                new RootMasterDetailPageMenuItem {Id = i++, Title = "Commands",TargetType = typeof(CommandPage.CommandListViewPage)},
                new RootMasterDetailPageMenuItem {Id = i++, Title = "Logs",TargetType = typeof(LogsPage.LogsPage)},
                new RootMasterDetailPageMenuItem {Id = i++, Title = "Settings",TargetType = typeof(SettingsPage.SettingsPage)}
            });

            Item = MenuItems.First();
        }


    }
}