using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Interfaces;

namespace ApptestSsh.Core.Services
{
    public class SQLiteFactory : ISQLiteFactory
    {
        private readonly IFileHelper _fileHelper;

        public SQLiteFactory(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }
        public string GetDatabasePath(string fileName)
        {
           return _fileHelper.GetLocalFilePath(fileName);
        }
    }
}
