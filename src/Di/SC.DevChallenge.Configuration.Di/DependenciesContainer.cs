using Autofac;
using Microsoft.Extensions.Options;
using SC.DevChallenge.Configuration.Abstractions;
using SC.DevChallenge.Configuration.DataAccess;

namespace SC.DevChallenge.Configuration.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => c.Resolve<IOptionsSnapshot<DbConfiguration>>().Value)
                .As<IDbConfiguration>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
