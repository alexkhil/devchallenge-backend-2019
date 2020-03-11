using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Integration.Tests.Fixtures;
using SC.DevChallenge.DataAccess.EF.Repositories;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Repositories
{
    public class PriceRepositoryTests : IClassFixture<PriceRepositoryTests.DbFixture>
    {
        private readonly PriceRepository sut;

        public PriceRepositoryTests(DbFixture dbFixture)
        {
            this.sut = new PriceRepository(dbFixture.AppDbContext);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExistingPrice_ShouldReturnOneRecord()
        {
            // Arrange
            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == nameof(Portfolio) &&
                x.Owner.Name == nameof(Owner) &&
                x.Instrument.Name == nameof(Instrument);

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().ContainSingle()
                .Which.Should().Be(42);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithNotExistingPrice_ShouldReturnEmptyResult()
        {
            // Arrange
            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == "Portfolio1" &&
                x.Owner.Name == "Owner1" &&
                x.Instrument.Name == "Instrument1";

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().BeEmpty();
        }

        public class DbFixture : DbFixtureBase
        {
            protected override async Task SeedDbAsync()
            {
                await this.AppDbContext.Prices.AddAsync(new Price
                {
                    Date = new DateTime(2018, 1, 1),
                    Timeslot = 1,
                    Value = 42,
                    Portfolio = new Portfolio { Name = nameof(Portfolio) },
                    Owner = new Owner { Name = nameof(Owner) },
                    Instrument = new Instrument { Name = nameof(Instrument) }
                });
                await this.AppDbContext.SaveChangesAsync();
            }
        }
    }
}
