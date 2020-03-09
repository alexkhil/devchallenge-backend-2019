using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.Api.Constants;
using SC.DevChallenge.DataAccess.EF;

namespace SC.DevChallenge.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDevChallengeDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.DevChallengeDb);
            return services.AddDbContextPool<AppDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
