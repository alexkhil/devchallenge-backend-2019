using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetAverage.Specifications;
using SC.DevChallenge.Queries.ViewModels;
using SC.DevChallenge.Tests;
using Xunit;

namespace SC.DevChallenge.Queries.Tests.Prices.GetAveragePrice
{
    public class GetAveragePriceQueryHandlerTests
    {
        private readonly Mock<IPriceRepository> priceRepositoryMock;
        private readonly Mock<IGetAveragePriceSpecification> specificationMock;
        private readonly Mock<IDateTimeConverter> dateTimeConverterMock;
        private readonly GetAveragePriceQueryHandler sut;

        public GetAveragePriceQueryHandlerTests()
        {
            this.priceRepositoryMock = new Mock<IPriceRepository>();
            this.specificationMock = new Mock<IGetAveragePriceSpecification>();
            this.dateTimeConverterMock = new Mock<IDateTimeConverter>();

            this.sut = new GetAveragePriceQueryHandler(
                this.priceRepositoryMock.Object,
                this.specificationMock.Object,
                this.dateTimeConverterMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ShouldCallGetTimeSlotStartDate()
        {
            // Arrange
            var request = CreateRequest();
            
            this.specificationMock
                .Setup(s => s.ToExpression(request))
                .Returns(It.IsAny<Expression<Func<Price, bool>>>());
            
            this.dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(request.Date))
                .Returns(It.IsAny<DateTime>());

            // Act
            await this.sut.Handle(request, default);

            // Assert
            this.dateTimeConverterMock.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallToExpression(int timeslot)
        {
            // Arrange
            var request = CreateRequest();
            
            this.specificationMock
                .Setup(s => s.ToExpression(request))
                .Returns(It.IsAny<Expression<Func<Price, bool>>>());
            
            this.dateTimeConverterMock
                .Setup(d => d.DateTimeToTimeSlot(request.Date))
                .Returns(timeslot);

            // Act
            await this.sut.Handle(request, default);

            // Assert
            this.specificationMock.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetAllAsync(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();
            
            this.specificationMock
                .Setup(s => s.ToExpression(request))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(expression,
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(It.IsAny<List<double>>());

            // Act
            await this.sut.Handle(request, default);

            // Assert
            this.priceRepositoryMock.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenNoPrice_ShouldBeNotFound(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();
            
            this.specificationMock
                .Setup(s => s.ToExpression(request))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(expression,
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(Array.Empty<double>().ToList());

            // Act
            var actual = await this.sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<AveragePriceViewModel>>();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenPriceExists_ShouldReturnCorrectAverage(
            Expression<Func<Price, bool>> expression,
            DateTime timeslotDate,
            double average)
        {
            // Arrange
            var expected = new AveragePriceViewModel
            {
                Date = timeslotDate,
                Price = average
            };

            var request = CreateRequest();

            this.dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(timeslotDate);

            this.specificationMock
                .Setup(s => s.ToExpression(request))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(expression,
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double> { average });

            // Act
            var actual = await this.sut.Handle(request, default);

            // Assert
            actual.Should().BeAssignableTo<DataHandlerResult<AveragePriceViewModel>>()
                .Which.Data.Should().BeEquivalentTo(expected);
        }

        private static GetAveragePriceQuery CreateRequest() =>
            new GetAveragePriceQuery
            {
                Date = new DateTime(2018, 1, 1),
                Instrument = "instrument",
                Owner = "owner",
                Portfolio = "portfolio"
            };
    }
}
