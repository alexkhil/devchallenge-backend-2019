using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
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

        private static string GetXmlPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }

        private static void GetSwaggerGenOptions(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "C# DevChallenge API"
            });

            options.IncludeXmlComments(GetXmlPath());
        }
    }
}