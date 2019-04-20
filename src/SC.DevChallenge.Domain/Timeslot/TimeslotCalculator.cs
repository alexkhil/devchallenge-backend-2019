namespace SC.DevChallenge.Domain.Timeslot
{
    public class TimeslotCalculator : ITimeslotCalculator
    {
        // Interquartile range  multiplier
        private const double IQRMultiplier = 1.5;

        /// <summary>
        /// Calculate lower filter bound
        /// </summary>
        /// <param name="pav">Price average values of quarter</param>
        /// <param name="interQuartileRange">Interquartile range</param>
        /// <returns>The value of lower bound</returns>
        public double GetLowerBound(
            double averagePriceOfQuarter,
            double interQuartileRange) =>
            averagePriceOfQuarter - IQRMultiplier * interQuartileRange;

        /// <summary>
        /// Calculate higher filter bound
        /// </summary>
        /// <param name="pav">Price average values of quarter</param>
        /// <param name="interQuartileRange">Interquartile range</param>
        /// <returns>The value of higher bound</returns>
        public double GetHigherBound(
            double averagePriceOfQuarter,
            double interQuartileRange) =>
            averagePriceOfQuarter + IQRMultiplier * interQuartileRange;
    }
}
