using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Photostudios.Tests;
using SC.DevChallenge.Api.Controllers;
using SC.DevChallenge.Dto;
using SC.DevChallenge.Mapping.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using Xunit;

namespace SC.DevChallenge.Api.Tests.Controllers
{
    public class PricesControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetAveragePrice_WhenCalled_ShouldMapDto(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetAveragePriceDto dto)
        {
            // Arrange
            mapperMock
                .Setup(m => m.Map<GetAveragePriceQuery>(It.IsAny<GetAveragePriceDto>()))
                .Returns(It.IsAny<GetAveragePriceQuery>());

            var sut = new PricesController(mapperMock.Object, mediatorMock.Object);

            // Act
            await sut.GetAveragePrice(dto);

            // Assert
            mapperMock.Verify(m => m.Map<GetAveragePriceQuery>(dto), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAveragePrice_WhenCalled_ShouldSendQuery(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetAveragePriceDto dto,
            GetAveragePriceQuery query)
        {
            // Arrange
            mapperMock
                .Setup(m => m.Map<GetAveragePriceQuery>(It.IsAny<GetAveragePriceDto>()))
                .Returns(query);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAveragePriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<IHandlerResult<AveragePriceDto>>());

            var sut = new PricesController(mapperMock.Object, mediatorMock.Object);

            // Act
            await sut.GetAveragePrice(dto);

            // Assert
            mediatorMock.Verify(m => m.Send(query, default), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAveragePrice_WhenNoPrices_ShouldBeNotFound(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetAveragePriceDto dto)
        {
            // Arrange
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAveragePriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<NotFoundHandlerResult<AveragePriceDto>>());

            var sut = new PricesController(mapperMock.Object, mediatorMock.Object);

            // Act
            var actual = await sut.GetAveragePrice(dto);

            // Assert
            actual.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAveragePrice_WhenExistsPrices_ShouldBeOkObjectResult(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetAveragePriceDto dto)
        {
            // Arrange
            var resultDto = AveragePriceDto.Create(DateTime.Now, 42);
            var result = new DataHandlerResult<AveragePriceDto>(resultDto);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAveragePriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var sut = new PricesController(mapperMock.Object, mediatorMock.Object);

            // Act
            var actual = await sut.GetAveragePrice(dto);

            // Assert
            actual.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<AveragePriceDto>().And.BeEquivalentTo(resultDto);
        }
    }
}
