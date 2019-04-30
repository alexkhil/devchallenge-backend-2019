using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Filters;
using SC.DevChallenge.Api.Infrastructure.ModelBinders;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AddCustomMvcExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services) =>
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));

                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            })
            .AddCustomJsonOptions()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .Services;
    }
}