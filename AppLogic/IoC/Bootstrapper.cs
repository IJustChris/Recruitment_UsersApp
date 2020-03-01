using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.IoC
{
    public static class Bootstrapper
    {
        private static IContainer _container { get; set; }

        public static void Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ContainerModule>();

            _container = builder.Build();
        }

        public static T GetInstance<T>()
        {
            if (_container == null)
            {
                Start();
            }

            return _container.Resolve<T>();
        }

        public static T GetInstance<T>(Parameter[] parameters)
        {
            if (_container == null)
            {
                Start();
            }

            return _container.Resolve<T>(parameters);
        }
    }
}
