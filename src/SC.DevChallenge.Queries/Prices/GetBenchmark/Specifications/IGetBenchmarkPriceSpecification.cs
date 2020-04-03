using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Queries.Abstractions;

namespace SC.DevChallenge.Queries.Prices.GetBenchmark.Specifications
{
    public interface IGetBenchmarkPriceSpecification :
        ISpecification<Price, GetBenchmarkPriceQuery>
    {
    }
}
