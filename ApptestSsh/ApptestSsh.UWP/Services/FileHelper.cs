using System.IO;
using Windows.Storage;
using Doods.StdFramework.Interfaces;

namespace ApptestSsh.UWP.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
