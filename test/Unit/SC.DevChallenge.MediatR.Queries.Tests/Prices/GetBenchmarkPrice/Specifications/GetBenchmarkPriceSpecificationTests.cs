using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetBenchmarkPrice.Specifications
{
    public class GetBenchmarkPriceSpecificationTests
    {
        [Theory]
        [AutoMoqData]
        public void IsSatisfyBy_PortfolioNameInvalid_ReturnFalse(
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            GetBenchmarkPriceQuery request,
            GetBenchmarkPriceSpecification sut)
        {
            // Arrange
            dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(It.IsAny<int>());

            var price = new Price
            {
                Portfolio = new Portfolio { Name = string.Empty }
            };

            // Act
            var actual = sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [AutoMoqData]
        public void IsSatisfyBy_TimeslotInvalid_ReturnFalse(
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            GetBenchmarkPriceQuery request,
            GetBenchmarkPriceSpecification sut)
        {
            // Arrange
            dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(1);

            var price = new Price
            {
                Portfolio = new Portfolio { Name = request.Portfolio },
                Timeslot = 42
            };

            // Act
            var actual = sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [AutoMoqData]
        public void IsSatisfyBy_Valid_ReturnTrue(
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            GetBenchmarkPriceQuery request,
            int timeslot,
            GetBenchmarkPriceSpecification sut)
        {
            // Arrange
            dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(timeslot);

            var price = new Price
            {
                Portfolio = new Portfolio { Name = request.Portfolio },
                Timeslot = timeslot
            };

            // Act
            var actual = sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeTrue();
        }
    }
}
