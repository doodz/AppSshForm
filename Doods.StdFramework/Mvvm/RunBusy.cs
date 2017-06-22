using System;

namespace Doods.StdFramework.Mvvm
{

    public interface IBuzy
    {
        bool IsBusy { get; }
        bool IsNotBusy { get; }
        int BusyCount { get; set; }
    }

    public class RunBusy : IDisposable
    {
        private readonly IBuzy _buzyObject;

        public RunBusy(IBuzy buzyObject)
        {
            _buzyObject = buzyObject;
            _buzyObject.BusyCount++;
        }

        public void Dispose()
        {
            _buzyObject.BusyCount--;
        }
    }
}
