using Autofac;

namespace Doods.StdFramework.ApplicationObjects
{
    public class AppSetup
    {
        private ContainerBuilder _containerBuilder;

        public ContainerBuilder ContainerBuilder
        {
            get => _containerBuilder;
            set => _containerBuilder = value;
        }

        private IContainer _contener;

        public void CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            //// Register Services
            //cb.RegisterType<CoreHelloFormsService>().As<IHelloFormsService>();

            //// Register View Models
            //cb.RegisterType<HomeViewModel>().SingleInstance();
        }

        public IContainer Build()
        {
            _contener = _containerBuilder.Build();
            AppContainer.Container = _contener;
            return _contener;
        }
    }
}