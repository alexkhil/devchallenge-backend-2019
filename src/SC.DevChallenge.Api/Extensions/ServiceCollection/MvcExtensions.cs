using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Filters;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddCustomMvc(
            this IServiceCollection services) =>
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddCustomJsonOptions()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .Services;
    }
}