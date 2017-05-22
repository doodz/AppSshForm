using System;
using Autofac;

namespace Doods.StdFramework.ApplicationObjects
{
    public static class AppContainer
    {
        private static IContainer _container;
        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    throw new Exception("Please initialize the container first, through SetupContainer");

                return _container;
            }
            internal set { _container = value; }
        }

    }
}
