using System;
using System.Globalization;
using FluentAssertions;
using SC.DevChallenge.Domain.Constants;
using SC.DevChallenge.Domain.DateTimeConverter;
using Xunit;

namespace SC.DevChallenge.DataAccess.Abstractions.Tests.DateTimeHelpers
{
    public class DateTimeConverterTests
    {
        private readonly DateTimeConverter sut;

        public DateTimeConverterTests()
        {
            sut = new DateTimeConverter();
        }

        [Fact]
        public void DateTimeToTimeSlot_WhenCalledWithStartDateTime_ShouldReturnZero()
        {
            // Arrange
            var date = new DateTime(2018, 1, 1);

            // Act
            var actual = sut.DateTimeToTimeSlot(date);

            // Assert
            actual.Should().Be(0);
        }

        [Theory]
        [InlineData(new object[] { "01/01/2018 00:00:00", 0 })]
        [InlineData(new object[] { "01/01/2018 01:46:40", 0 })]
        [InlineData(new object[] { "01/01/2018 02:46:40", 1 })]
        [InlineData(new object[] { "15/03/2018 17:33:40", 637 })]
        [InlineData(new object[] { "15/03/2018 17:34:40", 637 })]
        public void DateTimeToTimeSlot_WhenCalledWithDateTime_ShouldReturnCorrectTimeslot(
            string date,
            int expected)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(date, DateTimeFormat.Default, CultureInfo.InvariantCulture);

            // Act
            var actual = sut.DateTimeToTimeSlot(dateTime);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(new object[] { 0, "01/01/2018 00:00:00" })]
        [InlineData(new object[] { 1, "01/01/2018 02:46:40" })]
        public void GetTimeSlotStartDate_WhenCalled_ShouldReturnStartDateTime(
            int timeslot,
            string date)
        {
            // Arrange
            var expected = DateTime.ParseExact(date, DateTimeFormat.Default, CultureInfo.InvariantCulture);

            // Act
            var actual = sut.GetTimeSlotStartDate(timeslot);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
