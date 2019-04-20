using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQueryHandler : QueryHandlerBase<GetAggregatePriceQuery, AggregatePriceDto>
    {
        public override Task<IHandlerResult<AggregatePriceDto>> Handle(
            GetAggregatePriceQuery request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
