using System;
namespace Doods.StdFramework.Interfaces
{
    public interface ILogger
    {
        void Error(string msg);
        void Error(Exception e);

        void Warning(string msg);
        void Warning(Exception e);

        void Info(string msg);

        void Debug(string msg);
    }
}
