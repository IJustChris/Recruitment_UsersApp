using AppLogic.ViewModel;
using Autofac;

namespace AppLogic.IoC
{
    public class ViewModelModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>();

            base.Load(builder);
        }
    }
}
