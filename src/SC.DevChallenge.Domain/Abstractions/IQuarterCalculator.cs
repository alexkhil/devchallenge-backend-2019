namespace SC.DevChallenge.Domain.Abstractions
{
    public interface IQuarterCalculator
    {
        int GetQuarter(int quarterNumber, int timeslotElemCount);
    }
}