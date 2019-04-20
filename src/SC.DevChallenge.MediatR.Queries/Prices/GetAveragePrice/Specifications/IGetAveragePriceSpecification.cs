using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.Specification.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications
{
    public interface IGetAveragePriceSpecification :
        ISpecification<Price, GetAveragePriceQuery>
    {
    }
}