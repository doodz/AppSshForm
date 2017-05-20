using System.Runtime.CompilerServices;

namespace Doods.Framework.Interfaces
{
    public interface ILogManager
    {
        ILogger GetLog([CallerFilePath]string callerFilePath = "");
    }
}
