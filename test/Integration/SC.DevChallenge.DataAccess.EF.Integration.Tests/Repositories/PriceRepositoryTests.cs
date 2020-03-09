using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moq;
using SC.DevChallenge.Tests;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Integration.Tests.Fixtures;
using SC.DevChallenge.DataAccess.EF.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetAverage.Specifications;
using Xunit;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests.Repositories
{
    public class PriceRepositoryTests : IClassFixture<PricesDbFixture>
    {
        private readonly PriceRepository sut;

        public PriceRepositoryTests(PricesDbFixture pricesDbFixture)
        {
            this.sut = new PriceRepository(pricesDbFixture.AppDbContext);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExistingPrice_ShouldReturnOneRecord()
        {
            // Arrange
            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == nameof(Portfolio) &&
                x.Owner.Name == nameof(Owner) &&
                x.Instrument.Name == nameof(Instrument);

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().ContainSingle()
                .Which.Should().Be(42);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithNotExistingPrice_ShouldReturnEmptyResult()
        {
            // Arrange
            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == "Portfolio1" &&
                x.Owner.Name == "Owner1" &&
                x.Instrument.Name == "Instrument1";

            // Act
            var actual = await this.sut.GetAllAsync(filter, p => p.Value);

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
