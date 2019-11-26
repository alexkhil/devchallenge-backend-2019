using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SC.DevChallenge.Domain.Constants;
using SC.DevChallenge.Queries.ViewModels;
using Xunit;

namespace SC.DevChallenge.Api.Integration.Tests.Controllers
{
    public class PricesControllerTests : BaseControllerTest
    {
        [Fact]
        public async Task GetAveragePrice_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&owner=Google&instrument=CDS&date=15/03/2018 17:34:50",
                Path = "api/prices/average"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAveragePrice_NotExistingPortfolio_HttpStatusCodeNotFound()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=qwerty&owner=Microsoft&instrument=Deposit&date=15/03/2018 17:34:50",
                Path = "api/prices/average"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAveragePrice_ValidRequest_CorrectContent()
        {
            // Arrange
            var expected = new AveragePriceViewModel
            {
                Date = new DateTime(2018, 3, 15, 17, 26, 40),
                Price = 133.71
            };

            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&owner=Google&instrument=CDS&date=15/03/2018 17:34:50",
                Path = "api/prices/average"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<AveragePriceViewModel>(jsonContent, new IsoDateTimeConverter { DateTimeFormat = DateFormat.Default });

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetBenchmarkPrice_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&date=15/03/2018 17:34:50",
                Path = "api/prices/benchmark"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBenchmarkPrice_NotExistingPortfolio_HttpStatusCodeNotFound()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=qwerty&date=15/03/2018 17:34:50",
                Path = "api/prices/benchmark"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetBenchmarkPrice_ValidRequest_CorrectContent()
        {
            // Arranges
            var expected = new BenchmarkPriceViewModel
            {
                Date = new DateTime(2018, 3, 15, 17, 26, 40),
                Price = 133.71
            };

            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&date=15/03/2018 17:34:50",
                Path = "api/prices/benchmark"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BenchmarkPriceViewModel>(jsonContent, new IsoDateTimeConverter { DateTimeFormat = DateFormat.Default });

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAggregatePrice_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&startdate=06/10/2018 00:00:00&enddate=13/10/2018 00:00:00&resultpoints=7",
                Path = "api/prices/aggregate"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAggregatePrice_NotExistingPortfolio_HttpStatusCodeNotFound()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=qwerty&startdate=06/10/2018 00:00:00&enddate=13/10/2018 00:00:00&resultpoints=7",
                Path = "api/prices/aggregate"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAggregatePrice_ValidRequest_CorrectContent()
        {
            // Arranges
            var expected = new List<AggregatePriceViewModel>
            {
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 6, 19, 40, 0), Price = 381.47 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 7, 20, 40, 0), Price = 203.9 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 8, 21, 40, 0), Price = 289.25 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 9, 22, 40, 0), Price = 289.04 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 10, 23, 40, 0), Price = 262.48 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 12, 0, 40, 0), Price = 465.67 },
                new AggregatePriceViewModel { Date = new DateTime(2018, 10, 12, 22, 53, 20), Price = 187.77 }
            };

            var uriBuilder = new UriBuilder(this.Client.BaseAddress)
            {
                Query = "portfolio=Fannie Mae&startdate=06/10/2018 00:00:00&enddate=13/10/2018 00:00:00&resultpoints=7",
                Path = "api/prices/aggregate"
            };

            // Act
            var response = await this.Client.GetAsync(uriBuilder.Uri);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<AggregatePriceViewModel>>(jsonContent, new IsoDateTimeConverter { DateTimeFormat = DateFormat.Default });

            // Assert
            actual.Should().HaveCount(expected.Count);
        }
    }
}
