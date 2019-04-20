using System;
using System.Globalization;
using FluentAssertions;
using Photostudios.Tests;
using SC.DevChallenge.Dto;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using Xunit;

namespace SC.DevChallenge.Mapping.Tests.MappingProfiles
{
    public class GetAveragePriceProfileTests : MapperTestsBase
    {
        [Theory]
        [AutoMoqData]
        public void Map_MapFromCustomerEntity_IsValid(
            string instrument,
            string owner,
            string portfolio)
        {
            // Arrange
            var date = "01/01/2018 01:46:40";
            var expected = new GetAveragePriceQuery
            {
                Instrument = instrument,
                Date = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Owner = owner,
                Portfolio = portfolio
            };

            var dto = new GetAveragePriceDto
            {
                Instrument = instrument,
                Date = date,
                Owner = owner,
                Portfolio = portfolio
            };

            // Act
            var actual = Mapper.Map<GetAveragePriceQuery>(dto);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
