using System;
using SC.DevChallenge.Domain.Abstractions;

namespace SC.DevChallenge.Domain
{
    public class QuarterCalculator : IQuarterCalculator
    {
        public int GetQuarter(int quarterNumber, int timeslotElemCount) =>
            (int)Math.Ceiling(((quarterNumber * timeslotElemCount) - quarterNumber) / 4.0);
    }
}
