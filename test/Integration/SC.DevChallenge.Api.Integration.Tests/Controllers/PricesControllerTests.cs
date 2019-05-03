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
        public async Task GetBenchmark_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var client = factory.CreateClient();
            var uri = new Uri("/api/prices/benchmark?portfolio=Fannie Mae&date=15/03/2018 17:34:50", UriKind.Relative);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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
            var uri = new Uri("/api/prices/benchmark?portfolio=Fannie Mae&date=15/03/2018 17:34:50", UriKind.Relative);

            // Act
            var response = await client.GetAsync(uri);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BenchmarkPriceDto>(jsonContent, new IsoDateTimeConverter() { DateTimeFormat = DateTimeFormat.Default });

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
