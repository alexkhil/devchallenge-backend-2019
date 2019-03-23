using FluentAssertions;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetAveragePrice.Specifications
{
    public class GetAveragePriceSpecificationTests
    {
        [Theory]
        [AutoMoqData]
        public void IsSatisfiedBy_WhenSatisfied_ShouldReturnTrue(
            Price price,
            GetAveragePriceSpecification sut)
        {
            // Act
            var actual = sut.IsSatisfiedBy(
                price,
                price.Portfolio.Name,
                price.Owner.Name,
                price.Instrument.Name,
                price.Timeslot);

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
            var actual = sut.IsSatisfiedBy(
                price,
                string.Empty,
                string.Empty,
                string.Empty,
                -1);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
