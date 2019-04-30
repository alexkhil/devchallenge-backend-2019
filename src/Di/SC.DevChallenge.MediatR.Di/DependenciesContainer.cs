using Autofac;
using MediatR;
using SC.DevChallenge.MediatR.Behaviors;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;

namespace SC.DevChallenge.MediatR.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder
                .Register<ServiceFactory>(context =>
                {
                    var c = context.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });

            builder
                .RegisterGeneric(typeof(LoggingBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder
                .RegisterGeneric(typeof(PerformanceBehaviour<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder
                .RegisterAssemblyTypes(typeof(GetAveragePriceQuery).Assembly)
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
