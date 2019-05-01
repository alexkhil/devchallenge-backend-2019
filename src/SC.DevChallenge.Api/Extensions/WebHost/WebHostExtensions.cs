using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.DataAccess.EF;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;
using Z.EntityFramework.Extensions;

namespace SC.DevChallenge.Api.Extensions.WebHost
{
    public static class WebHostExtensions
    {
        public static async Task InitDbAsync(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var dbContextOptions = scope.ServiceProvider.GetRequiredService<DbContextOptions>();
                EntityFrameworkManager.ContextFactory = context => new AppDbContext(dbContextOptions);
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }
        }
    }
}
