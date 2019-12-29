using System;
using System.Collections.Generic;
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

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetTimeSlotStartDate(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            this.specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            this.dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(It.IsAny<DateTime>());

            var request = CreateRequest();

            // Act
            await this.sut.Handle(request, default);

            // Assert
            this.dateTimeConverterMock.Verify(d => d.GetTimeSlotStartDate(request.Date), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallToExpression(
            Expression<Func<Price, bool>> expression,
            int timeslot)
        {
            // Arrange
            this.dateTimeConverterMock
                .Setup(d => d.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(timeslot);

            this.specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            var request = CreateRequest();

            // Act
            await this.sut.Handle(request, default);

            // Assert
            this.specificationMock.Verify(s => s.ToExpression(request), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetAllAsync(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            this.specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double>());

            // Act
            await this.sut.Handle(CreateRequest(), default);

            // Assert
            this.priceRepositoryMock.Verify(
                r => r.GetAllAsync(expression, It.IsAny<Expression<Func<Price, double>>>()),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenNoPrice_ShouldBeNotFound(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            this.specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double>());

            // Act
            var actual = await this.sut.Handle(CreateRequest(), default);

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
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            this.priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
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
