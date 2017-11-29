﻿using ApptestSsh.UWP.Services;
using Autofac;
using Doods.StdFramework.Interfaces;

namespace ApptestSsh.UWP
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
