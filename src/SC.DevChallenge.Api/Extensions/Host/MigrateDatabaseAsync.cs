using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;

namespace SC.DevChallenge.Api.Extensions.Host
{
    public static partial class HostExtensions
    {
        public static async Task<IHost> MigrateDatabaseAsync<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await appDbContext.Database.EnsureDeletedAsync();
                await appDbContext.Database.MigrateAsync();

                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }

            return host;
        }
    }
}
