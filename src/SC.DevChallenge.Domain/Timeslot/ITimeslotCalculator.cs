namespace SC.DevChallenge.Domain.Timeslot
{
    public interface ITimeslotCalculator
    {
        double GetHigherBound(double averagePriceOfQuarter, double interQuartileRange);

        double GetLowerBound(double averagePriceOfQuarter, double interQuartileRange);
    }
}