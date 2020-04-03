using Microsoft.Extensions.DependencyInjection;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeProfiler(this IServiceCollection services) =>
            services.AddMiniProfiler()
                .AddEntityFramework()
                .Services;
    }
}
