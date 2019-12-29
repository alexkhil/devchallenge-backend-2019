using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetAverage.Specifications;
using SC.DevChallenge.Tests;
using Xunit;

namespace SC.DevChallenge.Queries.Tests.Prices.GetAveragePrice.Specifications
{
    public class GetAveragePriceSpecificationTests
    {
        [Theory, AutoMoqData]
        public void IsSatisfiedBy_WhenSatisfied_ShouldReturnTrue(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            Price price,
            GetAveragePriceSpecification sut)
        {
            // Arrange
            converterMock
                .Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(price.Timeslot);

            var request = new GetAveragePriceQuery
            {
                Portfolio = price.Portfolio.Name,
                Owner = price.Owner.Name,
                Instrument = price.Instrument.Name
            };

            // Act
            var actual = sut.IsSatisfiedBy(price, request);

            // Assert
            actual.Should().BeTrue();
        }

        [Theory, AutoMoqData]
        public void IsSatisfiedBy_WhenNotSatisfied_ShouldReturnFalse(
            Price price,
            GetAveragePriceSpecification sut)
        {
            // Arrange
            var request = new GetAveragePriceQuery
            {
                Portfolio = string.Empty,
                Owner = string.Empty,
                Instrument = string.Empty
            };

            // Act
            var actual = sut.IsSatisfiedBy(price, request);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
