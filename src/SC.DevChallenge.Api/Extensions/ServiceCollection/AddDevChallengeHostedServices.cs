using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Infrastructure.HostedServices;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeHostedServices(this IServiceCollection services) =>
            services.AddHostedService<MigratorHostedService>();
    }
}
