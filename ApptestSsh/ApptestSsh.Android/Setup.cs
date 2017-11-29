using Doods.StdFramework.ApplicationObjects;

namespace ApptestSsh.Droid
{
    public class Setup : AppSetup
    {

        public Setup()
        {
            ContainerBuilder = Bootstrapper.CreateContainer();
        }
    }
}