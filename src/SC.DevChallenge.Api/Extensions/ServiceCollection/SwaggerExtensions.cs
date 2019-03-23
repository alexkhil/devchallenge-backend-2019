using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomizedSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(GetSwaggerGenOptions);

            return services;
        }

        private static IEnumerable<string> GetXmlPaths()
        {
            return DependencyContext.Default.GetDefaultAssemblyNames()
                .Where(assembly => assembly.FullName.StartsWith("SC.DevChallenge", StringComparison.InvariantCulture))
                .Select(p => Path.Combine(AppContext.BaseDirectory, $"{p.Name}.xml"))
                .Where(p => File.Exists(p));
        }

        private static void GetSwaggerGenOptions(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "C# DevChallenge API"
            });

            foreach (var path in GetXmlPaths())
            {
                options.IncludeXmlComments(path);
            }
        }
    }
}