using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetAveragePrice
{
    public class GetAveragePriceQueryHandlerTests
    {
        private readonly Mock<IPriceRepository> priceRepositoryMock;
        private readonly Mock<IGetAveragePriceSpecification> specificationMock;
        private readonly Mock<IDateTimeConverter> dateTimeConverterMock;
        private readonly GetAveragePriceQueryHandler sut;

        public GetAveragePriceQueryHandlerTests()
        {
            priceRepositoryMock = new Mock<IPriceRepository>();
            specificationMock = new Mock<IGetAveragePriceSpecification>();
            dateTimeConverterMock = new Mock<IDateTimeConverter>();

            sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);
        }
        
        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetTimeSlotStartDate(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(It.IsAny<DateTime>());

            var request = CreateRequest();
            
            // Act
            await sut.Handle(request, default);

            // Assert
            dateTimeConverterMock.Verify(d => d.GetTimeSlotStartDate(request.Date), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallToExpression(
            Expression<Func<Price, bool>> expression,
            int timeslot)
        {
            // Arrange
            dateTimeConverterMock
                .Setup(d => d.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(timeslot);

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            var request = CreateRequest();
            
            // Act
            await sut.Handle(request, default);

            // Assert
            specificationMock.Verify(s => s.ToExpression(request), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetAllAsync(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double>());

            // Act
            await sut.Handle(CreateRequest(), default);

            // Assert
            priceRepositoryMock.Verify(
                r => r.GetAllAsync(expression, It.IsAny<Expression<Func<Price, double>>>()),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenNoPrice_ShouldBeNotFound(
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double>());

            // Act
            var actual = await sut.Handle(CreateRequest(), default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<AveragePriceDto>>();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenPriceExists_ShouldReturnCorrectAverage(
            Expression<Func<Price, bool>> expression,
            DateTime timeslotDate,
            double average)
        {
            // Arrange
            var expected = new AveragePriceDto
            {
                Date = timeslotDate,
                Price = average
            };

            var request = CreateRequest();

            dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(timeslotDate);

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(),
                    It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new List<double> { average });

            // Act
            var actual = await sut.Handle(request, default);

            // Assert
            actual.Should().BeAssignableTo<DataHandlerResult<AveragePriceDto>>()
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
