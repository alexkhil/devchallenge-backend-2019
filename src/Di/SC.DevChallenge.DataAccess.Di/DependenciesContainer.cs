using Autofac;
using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.Configuration.Abstractions;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Seeder;

namespace SC.DevChallenge.DataAccess.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AppDbContext>()
                .UsingConstructor(typeof(IDbConfiguration))
                .As<DbContext>()
                .AsSelf();

            builder
                .RegisterType<DbInitializer>()
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
