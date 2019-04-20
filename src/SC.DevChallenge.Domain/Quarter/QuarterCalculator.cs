using System;

namespace SC.DevChallenge.Domain.Quarter
{
    public class QuarterCalculator : IQuarterCalculator
    {
        public int GetQuarter(int quarterNumber, int timeslotElemCount) =>
            (int)Math.Ceiling(((quarterNumber * timeslotElemCount) - quarterNumber) / 4.0);
    }
}
