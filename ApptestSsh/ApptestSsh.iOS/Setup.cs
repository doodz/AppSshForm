using Doods.StdFramework.ApplicationObjects;

namespace ApptestSsh.iOS
{
    public class Setup : AppSetup
    {

        public Setup()
        {
            ContainerBuilder = Bootstrapper.CreateContainer();
        }
    }
}