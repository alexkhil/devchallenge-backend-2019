using System.Diagnostics.CodeAnalysis;
using Autofac;
using AutoMapper;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mapping.Mapper>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(ctx => new MapperConfiguration(x => x.AddMaps(typeof(Mapping.Mapper).Assembly)))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(ctx => new AutoMapper.Mapper(
                        ctx.Resolve<IConfigurationProvider>(),
                        ctx.Resolve<ILifetimeScope>().Resolve))
                .As<IMapper>()
                .SingleInstance();
        }
    }
}
