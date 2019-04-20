using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
using SC.DevChallenge.Domain.Quarter;
using SC.DevChallenge.Domain.Timeslot;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice.Specifications;
using Xunit;

namespace SC.DevChallenge.MediatR.Queries.Tests.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceQueryHandlerTests
    {
        private Mock<IGetBenchmarkPriceSpecification> specification;
        private Mock<IPriceRepository> priceRepository;
        private Mock<IDateTimeConverter> dateTimeConverter;
        private Mock<IQuarterCalculator> quarterCalculator;
        private Mock<ITimeslotCalculator> timeslotCalculator;

        public GetBenchmarkPriceQueryHandlerTests()
        {
            specification = new Mock<IGetBenchmarkPriceSpecification>();
            priceRepository = new Mock<IPriceRepository>();
            dateTimeConverter = new Mock<IDateTimeConverter>();
            quarterCalculator = new Mock<IQuarterCalculator>();
            timeslotCalculator = new Mock<ITimeslotCalculator>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallSpecification(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            var sut = CreateHandler();

            // Act

            await sut.Handle(request, default);

            // Assert
            specification.Verify(x => x.ToExpression(request), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenCalled_ShouldGetFilteredPrices(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            var sut = CreateHandler();

            // Act

            await sut.Handle(request, default);

            // Assert
            priceRepository.Verify(x => x.GetAllAsync(filter), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_WhenNoPrices_ReturnNotFound(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            var sut = CreateHandler();

            // Act

            var actual = await sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<BenchmarkPriceDto>>();
        }

        private GetBenchmarkPriceQueryHandler CreateHandler()
        {
            return new GetBenchmarkPriceQueryHandler(
                specification.Object,
                priceRepository.Object,
                dateTimeConverter.Object,
                quarterCalculator.Object,
                timeslotCalculator.Object);
        }
    }
}
