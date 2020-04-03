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
                Mock.Of<IDateTimeConverter>(),
                Mock.Of<IQuarterCalculator>(),
                Mock.Of<ITimeslotCalculator>());
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldCallSpecification(GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(request))
                .Returns(It.IsAny<Expression<Func<Price, bool>>>());

            // Act

            await this.sut.Handle(request, default);

            // Assert
            this.specification.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenCalled_ShouldGetFilteredPrices(
            Expression<Func<Price, bool>> filter,
            GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(request))
                .Returns(filter);

            this.priceRepository
                .Setup(x => x.GetAllAsync(filter))
                .ReturnsAsync(Array.Empty<Price>().ToList());

            // Act

            await this.sut.Handle(request, default);

            // Assert
            this.priceRepository.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task Handle_WhenNoPrices_ReturnNotFound(GetBenchmarkPriceQuery request)
        {
            // Arrange
            this.specification
                .Setup(x => x.ToExpression(request))
                .Returns(It.IsAny<Expression<Func<Price, bool>>>());
            
            this.priceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Price, bool>>>()))
                .ReturnsAsync(Array.Empty<Price>().ToList());

            // Act
            var actual = await this.sut.Handle(request, default);

            // Assert
            actual.Should().BeOfType<NotFoundHandlerResult<BenchmarkPriceViewModel>>();
        }
    }
}
