using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.DataAccess.EF;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeHealthChecks(this IServiceCollection services) =>
            services.AddHealthChecks()
                    .AddDbContextCheck<AppDbContext>()
                    .Services;
    }
}
