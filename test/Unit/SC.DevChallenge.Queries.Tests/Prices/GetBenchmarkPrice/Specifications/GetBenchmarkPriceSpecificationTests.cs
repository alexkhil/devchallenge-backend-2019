using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Queries.Prices.GetBenchmark;
using SC.DevChallenge.Queries.Prices.GetBenchmark.Specifications;
using SC.DevChallenge.Tests;
using Xunit;

namespace SC.DevChallenge.Queries.Tests.Prices.GetBenchmarkPrice.Specifications
{
    public class GetBenchmarkPriceSpecificationTests
    {
        private readonly Mock<IDateTimeConverter> dateTimeConverterMock;
        private readonly GetBenchmarkPriceSpecification sut;

        public GetBenchmarkPriceSpecificationTests()
        {
            this.dateTimeConverterMock = new Mock<IDateTimeConverter>();
            
            this.sut = new GetBenchmarkPriceSpecification(this.dateTimeConverterMock.Object);
        }
        
        [Theory, AutoMoqData]
        public void IsSatisfyBy_PortfolioNameInvalid_ReturnFalse(GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(request.Date))
                .Returns(It.IsAny<int>());

            var price = new Price
            {
                Portfolio = new Portfolio { Name = string.Empty }
            };

            // Act
            var actual = this.sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeFalse();
        }

        [Theory, AutoMoqData]
        public void IsSatisfyBy_TimeslotInvalid_ReturnFalse(GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(request.Date))
                .Returns(1);

            var price = new Price
            {
                Portfolio = new Portfolio { Name = request.Portfolio },
                Timeslot = 42
            };

            // Act
            var actual = this.sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeFalse();
        }

        [Theory, AutoMoqData]
        public void IsSatisfyBy_Valid_ReturnTrue(GetBenchmarkPriceQuery request, int timeslot)
        {
            // Arrange
            this.dateTimeConverterMock
                .Setup(c => c.DateTimeToTimeSlot(request.Date))
                .Returns(timeslot);

            var price = new Price
            {
                Portfolio = new Portfolio { Name = request.Portfolio },
                Timeslot = timeslot
            };

            // Act
            var actual = this.sut.IsSatisfiedBy(price, request);

            //Assert
            actual.Should().BeTrue();
        }
    }
}
