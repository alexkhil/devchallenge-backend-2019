using FluentAssertions;
using SC.DevChallenge.Domain.Timeslot;
using Xunit;

namespace SC.DevChallenge.Domain.Tests.Timeslot
{
    public class TimeslotCalculatorTests
    {
        [Theory]
        [InlineData(0.75, 0.5, 0)]
        public void GetLowerBound_WhenCalled_ReturnCorrect(
            double averagePriceOfQuarter,
            double interQuartileRange,
            double expected)
        {
            // Arrange
            var sut = new TimeslotCalculator();

            // Act
            var actual = sut.GetLowerBound(averagePriceOfQuarter, interQuartileRange);

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
            // Arrange
            var sut = new TimeslotCalculator();

            // Act
            var actual = sut.GetHigherBound(averagePriceOfQuarter, interQuartileRange);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
