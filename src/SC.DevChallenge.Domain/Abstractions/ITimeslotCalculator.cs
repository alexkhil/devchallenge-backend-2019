namespace SC.DevChallenge.Domain.Abstractions
{
    public interface ITimeslotCalculator
    {
        /// <summary>
        /// Calculate higher filter bound
        /// </summary>
        /// <param name="interQuartileRange">Interquartile range</param>
        /// <param name="averagePriceOfQuarter">Quarter average price</param>
        /// <returns>The value of higher bound</returns>
        double GetHigherBound(double averagePriceOfQuarter, double interQuartileRange);

        /// <summary>
        /// Calculate lower filter bound
        /// </summary>
        /// <param name="interQuartileRange">Interquartile range</param>
        /// <param name="averagePriceOfQuarter">Quarter average price</param>
        /// <returns>The value of lower bound</returns>
        double GetLowerBound(double averagePriceOfQuarter, double interQuartileRange);
    }
}