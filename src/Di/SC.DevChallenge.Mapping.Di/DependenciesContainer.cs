using System.Collections.Generic;
using Autofac;
using AutoMapper;

namespace SC.DevChallenge.Mapping.Di
{
    public class DependenciesContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(Mapper).Assembly)
                .AsImplementedInterfaces();

            builder
                .Register(ctx =>
                {
                    var profiles = ctx.Resolve<IEnumerable<Profile>>();
                    return new MapperConfiguration(x => x.AddProfiles(profiles));
                })
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(ctx =>
                {
                    return new AutoMapper.Mapper(
                        ctx.Resolve<IConfigurationProvider>(),
                        ctx.Resolve<ILifetimeScope>().Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
