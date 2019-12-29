using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Fixtures
{
    public class PricesDbFixture : IAsyncLifetime
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

        private async Task SeedDbAsync()
        {
            await this.AppDbContext.Prices.AddAsync(new Price
            {
                Date = new DateTime(2018, 1, 1),
                Timeslot = 1,
                Value = 42,
                Portfolio = new Portfolio {Name = nameof(Portfolio)},
                Owner = new Owner {Name = nameof(Owner)},
                Instrument = new Instrument {Name = nameof(Instrument)}
            });
            await this.AppDbContext.SaveChangesAsync();
        }
    }
}
