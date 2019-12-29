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
using SC.DevChallenge.Queries.Prices.GetBenchmark;
using SC.DevChallenge.Queries.Prices.GetBenchmark.Specifications;
using SC.DevChallenge.Queries.ViewModels;
using SC.DevChallenge.Tests;
using Xunit;

namespace SC.DevChallenge.Queries.Tests.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceQueryHandlerTests
    {
        private readonly Mock<IGetBenchmarkPriceSpecification> specification;
        private readonly Mock<IPriceRepository> priceRepository;
        private readonly GetBenchmarkPriceQueryHandler sut;

        public GetBenchmarkPriceQueryHandlerTests()
        {
            this.specification = new Mock<IGetBenchmarkPriceSpecification>();
            this.priceRepository = new Mock<IPriceRepository>();

            this.sut = new GetBenchmarkPriceQueryHandler(
                this.specification.Object,
                this.priceRepository.Object,
                new Mock<IDateTimeConverter>().Object,
                new Mock<IQuarterCalculator>().Object,
                new Mock<ITimeslotCalculator>().Object);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallSpecification(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            this.priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            // Act

            await this.sut.Handle(request, default);

            // Assert
            this.specification.Verify(x => x.ToExpression(request), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldGetFilteredPrices(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            this.priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            // Act

            await this.sut.Handle(request, default);

            // Assert
            this.priceRepository.Verify(x => x.GetAllAsync(filter), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenNoPrices_ReturnNotFound(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(It.IsAny<GetBenchmarkPriceQuery>()))
                .Returns(filter);

            this.priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(new List<Price>());

            // Act
            var actual = await this.sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<BenchmarkPriceViewModel>>();
        }
    }
}
