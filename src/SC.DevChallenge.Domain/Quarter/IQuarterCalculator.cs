namespace SC.DevChallenge.Domain.Quarter
{
    public interface IQuarterCalculator
    {
        int GetQuarter(int quarterNumber, int timeslotElemCount);
    }
}