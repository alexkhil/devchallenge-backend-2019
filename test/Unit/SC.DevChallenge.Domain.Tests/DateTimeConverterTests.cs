using System;
using System.Globalization;
using FluentAssertions;
using SC.DevChallenge.Domain.Constants;
using Xunit;

namespace SC.DevChallenge.Domain.Tests
{
    public class DateTimeConverterTests
    {
        private readonly DateTimeConverter sut = new DateTimeConverter();

        [Theory]
        [InlineData("01/01/2018 00:00:00", 0)]
        [InlineData("01/01/2018 01:46:40", 0)]
        [InlineData("01/01/2018 02:46:40", 1)]
        [InlineData("15/03/2018 17:33:40", 637)]
        [InlineData("15/03/2018 17:34:40", 637)]
        public void DateTimeToTimeSlot_WhenCalledWithDateTime_ShouldReturnCorrectTimeslot(string date, int expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(date, DateFormat.Default, CultureInfo.InvariantCulture);

            // Act
            var actual = this.sut.DateTimeToTimeSlot(dateTime);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("01/01/2018 00:00:00", 0)]
        [InlineData("01/01/2018 02:46:40", 1)]
        public void GetTimeSlotStartDate_WhenCalled_ShouldReturnStartDateTime(string date, int timeslot)
        {
            // Arrange
            var expected = DateTime.ParseExact(date, DateFormat.Default, CultureInfo.InvariantCulture);

            // Act
            var actual = this.sut.GetTimeSlotStartDate(timeslot);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
