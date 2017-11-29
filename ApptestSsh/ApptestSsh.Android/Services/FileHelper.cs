using Doods.StdFramework.Interfaces;
using System;
using System.IO;

namespace ApptestSsh.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public string GetDownloadPath()
        {
            var directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
                Android.OS.Environment.DirectoryDownloads);
            return directory;
            //return Path.Combine(directory, fileName);
        }

        public void StartAppFromFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}