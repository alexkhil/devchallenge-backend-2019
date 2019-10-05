using System.Diagnostics.CodeAnalysis;
using Autofac;
using SC.DevChallenge.Domain.Quarter;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(QuarterCalculator).Assembly)
                .AsImplementedInterfaces();
        }
    }
}
