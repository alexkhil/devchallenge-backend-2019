using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Fixtures
{
    public abstract class DbFixtureBase : IAsyncLifetime
    {
        public AppDbContext AppDbContext { get; private set; }
            
        public async Task InitializeAsync()
        {
            var dbContextOptions = new DbContextOptionsBuilder()
                .UseSqlite($"Data Source={Guid.NewGuid().ToString()}.db")
                .Options;

            this.AppDbContext = new AppDbContext(dbContextOptions);
            await this.AppDbContext.Database.EnsureCreatedAsync();
            await this.SeedDbAsync();
        }

        public async Task DisposeAsync()
        {
            await this.AppDbContext.Database.EnsureDeletedAsync();
            await this.AppDbContext.DisposeAsync();
        }

        protected abstract Task SeedDbAsync();
    }
}