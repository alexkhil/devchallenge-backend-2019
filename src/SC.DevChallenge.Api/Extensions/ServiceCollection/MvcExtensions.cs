using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SC.DevChallenge.Api.Filters;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddCustomizedMvc(
            this IServiceCollection services)
        {
            services.AddMvc(SetMvcOptions)
                    .AddJsonOptions(SetMvcJsonOptions)
                    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            return services;
        }

        private static void SetMvcJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                CamelCaseText = true
            });
        }

        private static void SetMvcOptions(MvcOptions options)
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
        }
    }
}