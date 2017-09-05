using System;

namespace Doods.StdFramework.Mvvm
{
    public class RunWithBool : IDisposable
    {
        //private Func<bool> _get;
        private readonly Action<bool> _set;
        public RunWithBool(/*Func<bool> @get,*/ Action<bool> @set)
        {

            //_get = @get;
            _set = @set;
            _set(true);
        }



        public void Dispose()
        {
            _set(false);
        }
    }
}