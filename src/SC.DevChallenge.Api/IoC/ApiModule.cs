using System.Diagnostics.CodeAnalysis;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.ExceptionHandling;
using SC.DevChallenge.Api.ExceptionHandling.Abstractions;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterExceptionHandling(builder);
            this.RedisterControllers(builder);
            this.RegisterMediator(builder);
        }

        private void RegisterExceptionHandling(ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionRequestHandler>()
                   .As<IExceptionRequestHandler>();

            builder.RegisterAssemblyTypes(this.ThisAssembly)
                   .AsClosedTypesOf(typeof(IExceptionHandler<>))
                   .As(typeof(IExceptionHandler<>));
        }

        private void RegisterMediator(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                   .As<IMediator>()
                   .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        private void RedisterControllers(ContainerBuilder builder) =>
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                   .Where(x => x.IsAssignableTo<ControllerBase>())
                   .PropertiesAutowired();
    }
}
