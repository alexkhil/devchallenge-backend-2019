using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Repositories;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class DataAccessModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => typeof(AppDbContext).Assembly;

        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterType<PriceRepository>().As<IPriceRepository>();
    }
}
