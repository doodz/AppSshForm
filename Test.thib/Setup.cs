using Doods.StdFramework.ApplicationObjects;

namespace Test.thib
{
    public class Setup : AppSetup
    {

        public Setup()
        {
            ContainerBuilder = Bootstrapper.CreateContainer();
        }
    }
}