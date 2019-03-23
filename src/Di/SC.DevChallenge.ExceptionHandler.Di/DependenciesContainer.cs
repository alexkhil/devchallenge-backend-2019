using Autofac;
using SC.DevChallenge.ExceptionHandler.ExceptionHandlers;

namespace SC.DevChallenge.ExceptionHandler.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(DefaultExceptionHandler).Assembly)
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}