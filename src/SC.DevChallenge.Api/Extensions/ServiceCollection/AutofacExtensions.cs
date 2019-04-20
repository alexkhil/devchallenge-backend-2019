using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using SC.DevChallenge.Mapping;
using SC.DevChallenge.Mapping.Abstractions;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AutofacExtensions
    {
        private const string AutofacModuleAssemblySuffix = ".Di";

        public static IContainer AddAutofacAfter(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterSerilog(configuration);

            var moduleAssemblies = DependencyContext.Default.GetDefaultAssemblyNames()
                .Where(assembly => assembly.FullName.EndsWith(AutofacModuleAssemblySuffix, StringComparison.InvariantCulture))
                .Select(Assembly.Load)
                .ToArray();

            builder.RegisterAssemblyModules(moduleAssemblies);
            builder.RegisterMapper();

            return builder.Build();
        }

        private static void RegisterMapper(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Mapper>()
                .As<IMapper>()
                .SingleInstance();
        }

        private static void RegisterSerilog(this ContainerBuilder builder, IConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration);
            builder.RegisterSerilog(loggerConfig);
        }
    }
}