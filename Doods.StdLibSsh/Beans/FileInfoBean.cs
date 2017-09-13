using Doods.StdFramework;
using Doods.StdFramework.Mvvm;
using System;

namespace Doods.StdLibSsh.Beans
{
    public class FileInfoBean : ObservableObject, IName
    {
        /// <summary>
        /// -rw-r--r--
        /// </summary>
        public string AccessRights { get; set; }

        public int Id { get; set; }

        public string Owner { get; set; }
        public string Group { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsFolder { get; set; }


        public string FullPath => $"{Path}/{Name}";
    }
}
