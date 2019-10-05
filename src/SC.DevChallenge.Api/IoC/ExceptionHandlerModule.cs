using System.Diagnostics.CodeAnalysis;
using Autofac;
using SC.DevChallenge.ExceptionHandler.ExceptionHandlers;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(DefaultExceptionHandler).Assembly)
                .AsImplementedInterfaces();
        }
    }
}
