using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class RouteOptionExtensions
    {
        public static IServiceCollection AddRouteOptions(
            this IServiceCollection services)
        {
            services.Configure<RouteOptions>(SetRouteOptions);

            return services;
        }

        private static void SetRouteOptions(RouteOptions options)
        {
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = false;
        }
    }
}