using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQueryHandler : QueryHandlerBase<GetAggregatePriceQuery, AggregatePriceDto>
    {
        private readonly IPriceRepository priceRepository;
        private readonly IDateTimeConverter dateTimeConverter;

        public GetAggregatePriceQueryHandler(
            IPriceRepository priceRepository,
            IDateTimeConverter dateTimeConverter)
        {
            this.priceRepository = priceRepository;
            this.dateTimeConverter = dateTimeConverter;
        }

        public override Task<IHandlerResult<AggregatePriceDto>> Handle(
            GetAggregatePriceQuery request,
            CancellationToken cancellationToken)
        {
            var startTimeSlot = dateTimeConverter.DateTimeToTimeSlot(request.StartDate);
            var endTimeSlot = dateTimeConverter.DateTimeToTimeSlot(request.EndDate);

            return Task.FromResult(NotFound());
        }
    }
}
