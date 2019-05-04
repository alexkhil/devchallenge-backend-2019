using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetAveragePrice.Specifications
{
    public class GetAveragePriceSpecificationTests
    {
        [Theory]
        [AutoMoqData]
        public void IsSatisfiedBy_WhenSatisfied_ShouldReturnTrue(
            [Frozen] Mock<IDateTimeConverter> converterMock,
            Price price,
            GetAveragePriceSpecification sut)
        {
            // Act
            converterMock
                .Setup(x => x.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(price.Timeslot);

            var request = new GetAveragePriceQuery
            {
                Portfolio = price.Portfolio.Name,
                Owner = price.Owner.Name,
                Instrument = price.Instrument.Name
            };

            var actual = sut.IsSatisfiedBy(
                price,
                request);

            // Assert
            actual.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public void IsSatisfiedBy_WhenNotSatisfied_ShouldReturnFalse(
            Price price,
            GetAveragePriceSpecification sut)
        {
            // Act
            var request = new GetAveragePriceQuery
            {
                Portfolio = string.Empty,
                Owner = string.Empty,
                Instrument = string.Empty
            };

            var actual = sut.IsSatisfiedBy(
                price,
                request);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
