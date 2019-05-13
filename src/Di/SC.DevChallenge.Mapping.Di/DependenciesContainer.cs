using Autofac;
using AutoMapper;

namespace SC.DevChallenge.Mapping.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mapper>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(ctx => new MapperConfiguration(x => x.AddMaps(typeof(Mapper).Assembly)))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(ctx => new AutoMapper.Mapper(ctx.Resolve<IConfigurationProvider>()))
                .As<IMapper>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
