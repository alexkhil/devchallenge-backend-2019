using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static class AddRouteOptionsExtensions
    {
        public static IServiceCollection AddRouteOptions(this IServiceCollection services) =>
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
    }
}