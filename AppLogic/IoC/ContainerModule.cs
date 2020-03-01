using Autofac;

namespace AppLogic.IoC
{
    public class ContainerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new ViewModelModule());

            base.Load(builder);
        }
    }
}