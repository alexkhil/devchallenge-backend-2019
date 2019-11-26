using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Queries.Abstractions;

namespace SC.DevChallenge.Queries.Prices.GetAverage.Specifications
{
    public interface IGetAveragePriceSpecification :
        ISpecification<Price, GetAveragePriceQuery>
    {
    }
}
