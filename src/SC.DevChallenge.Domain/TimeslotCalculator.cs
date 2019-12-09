using SC.DevChallenge.Domain.Abstractions;

namespace SC.DevChallenge.Domain
{
    public class TimeslotCalculator : ITimeslotCalculator
    {
        // Interquartile range  multiplier
        private const double IqrMultiplier = 1.5;

        /// <inheritdoc/>
        public double GetHigherBound(double averagePriceOfQuarter, double interQuartileRange) =>
            averagePriceOfQuarter + (IqrMultiplier * interQuartileRange);

        /// <inheritdoc/>
        public double GetLowerBound(double averagePriceOfQuarter, double interQuartileRange) =>
            averagePriceOfQuarter - (IqrMultiplier * interQuartileRange);
    }
}
