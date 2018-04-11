using ApptestSsh.Core.View.Base;
using Doods.StdFramework.Interfaces;
using PCLStorage;

namespace ApptestSsh.Core.View.LogsPage.ContentLogPage
{
    public class ContentLogPageViewModel : LocalViewModel
    {
        private IFile _localFile;

        public IFile LocalFile
        {
            get => _localFile;
            set
            {
                SetProperty(ref _localFile, value);

                Process();
            }
        }


        private string _fileContent;

        public string FileContent
        {
            get => _fileContent;
            set => SetProperty(ref _fileContent, value);
        }

        private void Process()
        {
            FileContent = LocalFile.ReadAllTextAsync().Result;
            if (string.IsNullOrEmpty(FileContent))
                FileContent = "File is empty";
        }

        public ContentLogPageViewModel(ILogger logger) : base(logger)
        {
        }
    }
}