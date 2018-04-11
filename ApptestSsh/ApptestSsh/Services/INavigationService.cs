using ApptestSsh.Core.DataBase;
using PCLStorage;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApptestSsh.Core.Services
{
    public interface INavigationService
    {
        Task GoBackToRoot();
        Task GotoLoginModal();
        Task GoToLogin();
        Task GoToLogin(Host host);
        Task GoToLoginModal(Host host);
        Task GoToHome();
        Task GoToHomeTabbed();
        Task GoToRootPage();
        Task GoToHostManagerPage();
        Task GoToModalHostManagerPage();
        Task GoToShellPage();
        Task GoToSettingsPage();

        Task GoToRootPageWindows();
        //Task GoToRootPageAndroid();
        Task GoToRootPageWindowsModal();
        //Task GoToRootPageAndroidModal();
        Task GoUpgradableListViewPageModal();
        void RemovePageFromHistory(Type type);

        Page GetWelcomeStartPage();

        INavigation Navigation { get; set; }
        Page CurrentPage { get; }
        Page CurrentModalPage { get; }

        void ClearHistory();
        Task GoUpgradableListViewPage();

        Task GoBack();
        Page GetRootPage();
        Task GoToAddCommandPage();
        Task GoToEditCommandPage(CommandSsh cmd);
        Task GoToContentLogPage(IFile file);
        Task GoToOmvServicesPage();
        Task GotoRddPage();
        Task GotoOmvUpdatePage();
        Task GotoOmvFileSystemsPage();
        Task GotoSharedsFolders();
    }
}