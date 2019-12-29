using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SC.DevChallenge.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Integration.Tests.Fixtures;
using SC.DevChallenge.DataAccess.EF.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetAverage.Specifications;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Repositories
{
    public class PriceRepositoryTests : IClassFixture<PricesDbFixture>
    {
        private readonly PriceRepository sut;

        public PriceRepositoryTests(PricesDbFixture pricesDbFixture)
        {
            this.sut = new PriceRepository(pricesDbFixture.AppDbContext);
        }

        [Theory, AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithExistingPrice_ShouldReturnOneRecord(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            GetAveragePriceSpecification specification)
        {
            // Arrange
            converterMock.Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>())).Returns(1);
            var request = new GetAveragePriceQuery
            {
                Portfolio = nameof(Portfolio), Owner = nameof(Owner), Instrument = nameof(Instrument)
            };
            var filter = specification.ToExpression(request);

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().ContainSingle()
                .Which.Should().Be(42);
        }

        [Theory, AutoMoqData]
        public async Task GetAllAsync_WhenCalledWithNotExistingPrice_ShouldReturnEmptyResult(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            GetAveragePriceSpecification specification)
        {
            // Arrange
            converterMock.Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>())).Returns(2);
            var request = new GetAveragePriceQuery
            {
                Portfolio = "Portfolio1", Owner = "Owner1", Instrument = "Instrument1"
            };
            var filter = specification.ToExpression(request);

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
