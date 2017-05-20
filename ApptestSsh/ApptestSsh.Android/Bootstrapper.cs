using ApptestSsh.Core.Services;
using ApptestSsh.Droid.Services;
using Autofac;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Interfaces;

namespace ApptestSsh.Droid
{
    public class Bootstrapper
    {
        public static ContainerBuilder CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<FileHelper>().As<IFileHelper>().SingleInstance();

            return builder;
        }
    }
}