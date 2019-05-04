using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetAveragePrice
{
    public class GetAveragePriceQueryHandlerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetTimeSlotStartDate(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(It.IsAny<DateTime>());

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            await sut.Handle(request, default);

            // Assert
            dateTimeConverterMock.Verify(d => d.GetTimeSlotStartDate(request.Date), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallToExpression(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            Expression<Func<Price, bool>> expression,
            int timeslot)
        {
            // Arrange
            var request = CreateRequest();

            dateTimeConverterMock
                .Setup(d => d.DateTimeToTimeSlot(It.IsAny<DateTime>()))
                .Returns(timeslot);

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            await sut.Handle(request, default);

            // Assert
            specificationMock.Verify(s => s.ToExpression(request), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallGetAllAsync(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(), It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(Enumerable.Empty<double>());

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            await sut.Handle(request, default);

            // Assert
            priceRepositoryMock.Verify(r => r.GetAllAsync(expression, It.IsAny<Expression<Func<Price, double>>>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenNoPrice_ShouldBeNotFound(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(), It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(Enumerable.Empty<double>());

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            var actual = await sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<AveragePriceDto>>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenPriceExists_ShouldBeData(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            Expression<Func<Price, bool>> expression)
        {
            // Arrange
            var request = CreateRequest();

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(), It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new double[] { 42 });

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            var actual = await sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<DataHandlerResult<AveragePriceDto>>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenPriceExists_ShouldReturnCorrectAverage(
            [Frozen] Mock<IPriceRepository> priceRepositoryMock,
            [Frozen] Mock<IGetAveragePriceSpecification> specificationMock,
            [Frozen] Mock<IDateTimeConverter> dateTimeConverterMock,
            Expression<Func<Price, bool>> expression,
            DateTime timeslotDate,
            double average)
        {
            // Arrange
            var expected = AveragePriceDto.Create(timeslotDate, average);

            var request = CreateRequest();

            dateTimeConverterMock
                .Setup(d => d.GetTimeSlotStartDate(It.IsAny<DateTime>()))
                .Returns(timeslotDate);

            specificationMock
                .Setup(s => s.ToExpression(It.IsAny<GetAveragePriceQuery>()))
                .Returns(expression);

            priceRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>(), It.IsAny<Expression<Func<Price, double>>>()))
                .ReturnsAsync(new double[] { average });

            var sut = new GetAveragePriceQueryHandler(
                priceRepositoryMock.Object,
                specificationMock.Object,
                dateTimeConverterMock.Object);

            // Act
            var actual = await sut.Handle(request, default) as DataHandlerResult<AveragePriceDto>;

            // Assert
            actual.Data.Should().BeEquivalentTo(expected);
        }

        private static GetAveragePriceQuery CreateRequest()
        {
            return new GetAveragePriceQuery
            {
                Date = new DateTime(2018, 1, 1),
                Instrument = "instrument",
                Owner = "owner",
                Portfolio = "portfolio"
            };
        }
    }
}
