using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using SC.DevChallenge.Domain.Date;
using Swashbuckle.AspNetCore.Swagger;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class SwaggerExtensions
    {
        private const string Version = "v1";

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(options =>
            {
                var assembly = typeof(Startup).Assembly;
                var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                options.DescribeAllEnumsAsStrings();
                options.DescribeAllParametersInCamelCase();
                options.DescribeStringEnumsInCamelCase();
                options.MapType<DateTime>(() => new Schema { Type = "string", Pattern = DateTimeFormat.Pattern });

                options.SwaggerDoc(Version, new Info
                {
                    Version = Version,
                    Title = assemblyProduct,
                    Description = assemblyDescription
                });

                foreach (var path in GetXmlPaths())
                {
                    options.IncludeXmlComments(path);
                }
            });

        private static IEnumerable<string> GetXmlPaths()
        {
            return DependencyContext.Default.GetDefaultAssemblyNames()
                .Where(assembly => assembly.FullName.StartsWith("SC.DevChallenge", StringComparison.InvariantCulture))
                .Select(p => Path.Combine(AppContext.BaseDirectory, $"{p.Name}.xml"))
                .Where(p => File.Exists(p));
        }
    }
}