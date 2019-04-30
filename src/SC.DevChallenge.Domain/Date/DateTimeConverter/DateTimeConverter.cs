using System;

namespace SC.DevChallenge.Domain.Date.DateTimeConverter
{
    public class DateTimeConverter : IDateTimeConverter
    {
        private const int TimeslotSize = 10000; // sec
        private static DateTime startDate = new DateTime(2018, 1, 1);

        public int DateTimeToTimeSlot(DateTime dateTime)
        {
            var startDateTimeSpan = TimeSpan.FromTicks(startDate.Ticks);
            var dateTimeSpan = TimeSpan.FromTicks(dateTime.Ticks);
            return Convert.ToInt32(Math.Floor(dateTimeSpan.Subtract(startDateTimeSpan).TotalSeconds / TimeslotSize));
        }

        public DateTime GetTimeSlotStartDate(int timeslot) =>
            startDate.AddSeconds(timeslot * TimeslotSize);

        public DateTime GetTimeSlotStartDate(DateTime dateTime)=>
            GetTimeSlotStartDate(DateTimeToTimeSlot(dateTime));
    }
}
