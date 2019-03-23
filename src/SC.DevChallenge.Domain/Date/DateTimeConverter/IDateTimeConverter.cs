using System;

namespace SC.DevChallenge.Domain.Date.DateTimeConverter
{
    public interface IDateTimeConverter
    {
        int DateTimeToTimeSlot(DateTime dateTime);

        DateTime GetTimeSlotStartDate(int timeslot);
    }
}