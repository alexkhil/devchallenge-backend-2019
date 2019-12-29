using FluentAssertions;
using Xunit;

namespace SC.DevChallenge.Domain.Tests
{
    public class TimeslotCalculatorTests
    {
        private readonly TimeslotCalculator sut;

        public TimeslotCalculatorTests()
        {
            this.sut = new TimeslotCalculator();
        }

        [Theory]
        [InlineData(0.75, 0.5, 0)]
        public void GetLowerBound_WhenCalled_ReturnCorrect(
            double averagePriceOfQuarter,
            double interQuartileRange,
            double expected)
        {
            // Act
            var actual = this.sut.GetLowerBound(averagePriceOfQuarter, interQuartileRange);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0.75, 0.5, 1.5)]
        public void GetHigherBound_WhenCalled_ReturnCorrect(
            double averagePriceOfQuarter,
            double interQuartileRange,
            double expected)
        {
            // Act
            var actual = this.sut.GetHigherBound(averagePriceOfQuarter, interQuartileRange);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
