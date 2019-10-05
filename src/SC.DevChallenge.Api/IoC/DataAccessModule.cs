using System.Diagnostics.CodeAnalysis;
using Autofac;
using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.Configuration.Abstractions;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Repositories;
using SC.DevChallenge.DataAccess.EF.Seeder;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(ctx =>
                {
                    var configuration = ctx.Resolve<IDbConfiguration>();
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                    optionsBuilder.UseSqlServer(configuration.ConnectionString);
                    return optionsBuilder.Options;
                })
                .As<DbContextOptions>()
                .SingleInstance();

            builder
                .RegisterType<AppDbContext>()
                .As<DbContext>()
                .AsSelf();

            builder
                .RegisterType<DbInitializer>()
                .AsImplementedInterfaces();

            builder
                .RegisterType<PriceRepository>()
                .AsImplementedInterfaces();
        }
    }
}
