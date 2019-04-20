using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQueryHandler : QueryHandlerBase<GetAggregatePriceQuery, AggregatePriceDto>
    {
        private readonly IPriceRepository priceRepository;

        public GetAggregatePriceQueryHandler(
            IPriceRepository priceRepository)
        {
            this.priceRepository = priceRepository;
        }

        public override Task<IHandlerResult<AggregatePriceDto>> Handle(
            GetAggregatePriceQuery request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
