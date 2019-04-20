using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using SC.DevChallenge.Mapping;
using SC.DevChallenge.Mapping.Abstractions;

namespace SC.DevChallenge.Api
{
    internal class AutofacModule : Autofac.Module
    {
        private const string AutofacModuleAssemblySuffix = ".Di";

        protected override void Load(ContainerBuilder builder)
        {
            var moduleAssemblies = DependencyContext.Default.GetDefaultAssemblyNames()
                .Where(assembly => assembly.FullName.EndsWith(AutofacModuleAssemblySuffix, StringComparison.InvariantCulture))
                .Select(Assembly.Load)
                .ToArray();

            builder.RegisterAssemblyModules(moduleAssemblies);

            builder
                .RegisterType<Mapper>()
                .As<IMapper>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}