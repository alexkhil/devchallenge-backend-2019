using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SC.DevChallenge.Domain.Date;
using SC.DevChallenge.Dto;
using Xunit;

namespace SC.DevChallenge.Api.Integration.Tests.Controllers
{
    public class PricesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public PricesControllerTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetAveragePrice_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var client = factory.CreateClient();
            var uriBuilder = new UriBuilder(client.BaseAddress);
            uriBuilder.Query = "portfolio=Fannie Mae&owner=Microsoft&instrument=Deposit&date=15/03/2018 17:34:50";
            uriBuilder.Path = "api/prices/average";

            // Act
            var response = await client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAveragePrice_NotExistingPortfolio_HttpStatusCodeNotFound()
        {
            // Arrange
            var client = factory.CreateClient();
            var uriBuilder = new UriBuilder(client.BaseAddress);
            uriBuilder.Query = "portfolio=qwerty&owner=Microsoft&instrument=Deposit&date=15/03/2018 17:34:50";
            uriBuilder.Path = "api/prices/average";

            // Act
            var response = await client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetBenchmark_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var client = factory.CreateClient();
            var uriBuilder = new UriBuilder(client.BaseAddress);
            uriBuilder.Query = "portfolio=Fannie Mae&date=15/03/2018 17:34:50";
            uriBuilder.Path = "api/prices/benchmark";

            // Act
            var response = await client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBenchmark_NotExistingPortfolio_HttpStatusCodeNotFound()
        {
            // Arrange
            var client = factory.CreateClient();
            var uriBuilder = new UriBuilder(client.BaseAddress);
            uriBuilder.Query = "portfolio=qwerty&date=15/03/2018 17:34:50";
            uriBuilder.Path = "api/prices/benchmark";

            // Act
            var response = await client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetBenchmark_ValidRequest_CorrectContent()
        {
            // Arranges
            var expected = new BenchmarkPriceDto
            {
                Date = new DateTime(2018, 3, 15, 17, 26, 40),
                Price = 133.71
            };

            var client = factory.CreateClient();
            var uriBuilder = new UriBuilder(client.BaseAddress);
            uriBuilder.Query = "portfolio=Fannie Mae&date=15/03/2018 17:34:50";
            uriBuilder.Path = "api/prices/benchmark";

            // Act
            var response = await client.GetAsync(uriBuilder.Uri);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BenchmarkPriceDto>(jsonContent, new IsoDateTimeConverter() { DateTimeFormat = DateTimeFormat.Default });

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
