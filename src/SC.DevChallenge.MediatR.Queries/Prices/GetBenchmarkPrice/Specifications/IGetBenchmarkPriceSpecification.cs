using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice;
using SC.DevChallenge.Specification.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice.Specifications
{
    public interface IGetBenchmarkPriceSpecification :
        ISpecification<Price, GetBenchmarkPriceQuery>
    {
    }
}
