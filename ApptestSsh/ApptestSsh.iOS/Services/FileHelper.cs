using Doods.StdFramework.Interfaces;
using System;
using System.IO;

namespace ApptestSsh.iOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public string GetDownloadPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
        }

        public void StartAppFromFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}