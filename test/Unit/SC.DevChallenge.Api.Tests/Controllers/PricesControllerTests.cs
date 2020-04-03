using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SC.DevChallenge.Api.Controllers;
using SC.DevChallenge.Api.Requests.Prices;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.ViewModels;
using SC.DevChallenge.Tests;
using Xunit;

namespace SC.DevChallenge.Api.Tests.Controllers
{
    public class PricesControllerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IMediator> mediatorMock;
        private readonly PricesController sut;

        public PricesControllerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.mediatorMock = new Mock<IMediator>();
            this.sut = new PricesController(
                this.mapperMock.Object,
                this.mediatorMock.Object);
        }

        [Theory, AutoMoqData]
        public async Task GetAveragePrice_WhenCalled_ShouldMapDto(GetAveragePriceDto dto)
        {
            // Arrange
            this.mapperMock
                .Setup(m => m.Map<GetAveragePriceQuery>(dto))
                .Returns(It.IsAny<GetAveragePriceQuery>());

            // Act
            await this.sut.GetAverage(dto);

            // Assert
            this.mapperMock.Verify(x => x.Map<GetAveragePriceQuery>(dto), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task GetAveragePrice_WhenCalled_ShouldSendQuery(
            GetAveragePriceDto dto,
            GetAveragePriceQuery query)
        {
            // Arrange
            this.mapperMock
                .Setup(m => m.Map<GetAveragePriceQuery>(dto))
                .Returns(query);

            this.mediatorMock
                .Setup(m => m.Send(query, default))
                .ReturnsAsync(It.IsAny<IHandlerResult<AveragePriceViewModel>>());

            // Act
            await this.sut.GetAverage(dto);

            // Assert
            this.mediatorMock.VerifyAll();
        }

        [Theory, AutoMoqData]
        public async Task GetAveragePrice_WhenNoPrices_ShouldBeNotFound(GetAveragePriceDto dto)
        {
            // Arrange
            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAveragePriceQuery>(), default))
                .ReturnsAsync(It.IsAny<NotFoundHandlerResult<AveragePriceViewModel>>());

            // Act
            var actual = await this.sut.GetAverage(dto);

            // Assert
            actual.Should().BeOfType<NotFoundResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetAveragePrice_WhenExistsPrices_ShouldBeOkObjectResult(
            AveragePriceViewModel resultDto,
            GetAveragePriceDto dto)
        {
            // Arrange
            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAveragePriceQuery>(), default))
                .ReturnsAsync(new DataHandlerResult<AveragePriceViewModel>(resultDto));

            // Act
            var actual = await this.sut.GetAverage(dto);

            // Assert
            actual.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(resultDto);
        }
    }
}
