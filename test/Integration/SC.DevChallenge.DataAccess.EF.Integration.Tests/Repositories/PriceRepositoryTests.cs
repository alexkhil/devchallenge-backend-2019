using System;
using System.Threading.Tasks;
using FluentAssertions;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Repositories;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Repositories
{
    public class PriceRepositoryTests : IDisposable
    {
        private readonly LocalDbContext dbContext;
        private bool disposedValue = false;

        public PriceRepositoryTests()
        {
            dbContext = new LocalDbContext();
            SeedDb().GetAwaiter().GetResult();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithExistingPrice_ShouldReturnOneRecord(
            GetAveragePriceSpecification specification)
        {
            // Arrange
            var filter = specification.ToExpression("Portfolio", "Owner", "Instrument", 1);
            var sut = new PriceRepository(dbContext);

            // Act
            var actual = await sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().ContainSingle().Which.Should().Be(42);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithNotExistingPrice_ShouldReturnEmptyResult(
            GetAveragePriceSpecification specification)
        {
            // Arrange
            var filter = specification.ToExpression("Portfolio1", "Owner1", "Instrument1", 2);
            var sut = new PriceRepository(dbContext);

            // Act
            var actual = await sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().BeEmpty();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        private async Task SeedDb()
        {
            await dbContext.Prices.AddAsync(new Price
            {
                Date = new DateTime(2018, 1, 1),
                Timeslot = 1,
                Value = 42,
                Portfolio = new Portfolio { Name = "Portfolio" },
                Owner = new Owner { Name = "Owner" },
                Instrument = new Instrument { Name = "Instrument" }
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
