using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.DataAccess.EF;

namespace SC.DevChallenge.Api.Infrastructure.HostedServices
{
    public class MigratorHostedService : IHostedService
    {
        private readonly ILogger<MigratorHostedService> logger;
        private readonly AppDbContext appDbContext;

        public MigratorHostedService(ILogger<MigratorHostedService> logger, AppDbContext appDbContext)
        {
            this.logger = logger;
            this.appDbContext = appDbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.appDbContext.Database.EnsureDeletedAsync(cancellationToken);

            this.logger.LogInformation("Creating Db...");
            await this.appDbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
