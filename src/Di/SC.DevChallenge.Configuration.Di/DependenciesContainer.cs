using Autofac;
using SC.DevChallenge.Configuration.Abstractions;
using SC.DevChallenge.Configuration.DataAccess;

namespace SC.DevChallenge.Configuration.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register<IDbConfiguration>(c => c.Resolve<DbConfiguration>())
                .SingleInstance();

            base.Load(builder);
        }
    }
}
