using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using SC.DevChallenge.Domain;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class DomainModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => typeof(QuarterCalculator).Assembly;

        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterAssemblyTypes(this.ThisAssembly).AsImplementedInterfaces();
    }
}
