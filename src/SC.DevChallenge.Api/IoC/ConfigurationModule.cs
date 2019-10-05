using System.Diagnostics.CodeAnalysis;
using Autofac;
using SC.DevChallenge.Configuration.Abstractions;
using SC.DevChallenge.Configuration.DataAccess;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register<IDbConfiguration>(c => c.Resolve<DbConfiguration>())
                .SingleInstance();
        }
    }
}
