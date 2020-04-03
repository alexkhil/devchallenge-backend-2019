using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRouteOptions(this IServiceCollection services)
        {
            return services.Configure<RouteOptions>(SetupRouteOptions);

            static void SetupRouteOptions(RouteOptions options)
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = false;
            }
        }
    }
}