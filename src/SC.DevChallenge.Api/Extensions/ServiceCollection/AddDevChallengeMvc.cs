using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Filters;
using SC.DevChallenge.Api.Infrastructure.ModelBinders;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeMvc(this IServiceCollection services)
        {
            return services.AddControllers(SetupMvcOptions)
                .AddJsonOptions(SetupJsonOptions)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .Services;

            static void SetupMvcOptions(MvcOptions options)
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            }

            static void SetupJsonOptions(JsonOptions options)
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.MaxDepth = 16;
            }
        }
    }
}