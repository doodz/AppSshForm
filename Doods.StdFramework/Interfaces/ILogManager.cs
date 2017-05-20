using System.Runtime.CompilerServices;

namespace Doods.StdFramework.Interfaces
{
    public interface ILogManager
    {
        ILogger GetLog([CallerFilePath]string callerFilePath = "");
    }
}
