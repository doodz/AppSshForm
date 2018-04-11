using ApptestSsh.Core.View.Base;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;

namespace ApptestSsh.Core.View.SettingsPage
{
    public class SettingsPageViewModel : LocalViewModel
    {
        private string _downloadPath;

        public string DownloadPath
        {
            get => _downloadPath;
            set => SetProperty(ref _downloadPath, value);
        }

        public SettingsPageViewModel(ILogger logger) : base(logger)
        {
            GetDownloadPath();
        }


        private void GetDownloadPath()
        {
            var fileHelper = AppContainer.Container.Resolve<IFileHelper>();
            DownloadPath = fileHelper.GetDownloadPath();
        }
    }
}