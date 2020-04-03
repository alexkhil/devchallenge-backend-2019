using FluentAssertions;
using Xunit;

namespace SC.DevChallenge.Domain.Tests
{
    public class QuarterCalculatorTests
    {
        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(3, 1, 0)]
        [InlineData(1, 5, 1)]
        [InlineData(3, 5, 3)]
        public void GetQuarter_WhenCalled_ReturnCorrect(int quarterNumber, int timeslotElemCount, int expected)
        {
            // Arrange
            var sut = new QuarterCalculator();

            // Act
            var actual = sut.GetQuarter(quarterNumber, timeslotElemCount);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
