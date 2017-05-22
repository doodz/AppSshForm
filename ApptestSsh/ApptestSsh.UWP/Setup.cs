using System;
using Doods.StdFramework.ApplicationObjects;

namespace ApptestSsh.UWP
{
    public class Setup : AppSetup
    {

        public Setup()
        {
            ContainerBuilder = Bootstrapper.CreateContainer();
        }
    }
}
