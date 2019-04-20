using Autofac;
using SC.DevChallenge.Domain.Quarter;

namespace SC.DevChallenge.Domain.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(QuarterCalculator).Assembly)
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
