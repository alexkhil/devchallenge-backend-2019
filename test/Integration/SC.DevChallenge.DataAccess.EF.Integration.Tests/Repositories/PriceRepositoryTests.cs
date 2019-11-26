using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Repositories;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Repositories
{
    public class PriceRepositoryTests : IDisposable
    {
        private readonly AppDbContext dbContext;
        private bool disposedValue;

        public PriceRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("test");
            dbContext = new AppDbContext(optionsBuilder.Options);
            SeedDb().GetAwaiter().GetResult();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithExistingPrice_ShouldReturnOneRecord(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            GetAveragePriceSpecification specification)
        {
            // Arrange
            converterMock.Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>())).Returns(1);
            var request = new GetAveragePriceQuery
            {
                Portfolio = nameof(Portfolio),
                Owner = nameof(Owner),
                Instrument = nameof(Instrument)
            };
            var filter = specification.ToExpression(request);
            var sut = new PriceRepository(dbContext);

            // Act
            var actual = await sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().ContainSingle().Which.Should().Be(42);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithNotExistingPrice_ShouldReturnEmptyResult(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            GetAveragePriceSpecification specification)
        {
            // Arrange
            converterMock.Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>())).Returns(2);
            var request = new GetAveragePriceQuery
            {
                Portfolio = "Portfolio1",
                Owner = "Owner1",
                Instrument = "Instrument1"
            };
            var filter = specification.ToExpression(request);
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
            if (disposedValue)
            {
                return;
            }

            if (disposing)
            {
                dbContext.Dispose();
            }

            disposedValue = true;
        }

        private async Task SeedDb()
        {
            await dbContext.Prices.AddAsync(new Price
            {
                Date = new DateTime(2018, 1, 1),
                Timeslot = 1,
                Value = 42,
                Portfolio = new Portfolio { Name = nameof(Portfolio) },
                Owner = new Owner { Name = nameof(Owner) },
                Instrument = new Instrument { Name = nameof(Instrument) }
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
