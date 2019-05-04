using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice
{
    public class GetAveragePriceQueryHandler : QueryHandlerBase<GetAveragePriceQuery, AveragePriceDto>
    {
        private readonly IPriceRepository priceRepository;
        private readonly IGetAveragePriceSpecification getAveragePriceSpecification;
        private readonly IDateTimeConverter dateTimeConverter;

        public GetAveragePriceQueryHandler(
            IPriceRepository priceRepository,
            IGetAveragePriceSpecification getAveragePriceSpecification,
            IDateTimeConverter dateTimeConverter)
        {
            this.priceRepository = priceRepository;
            this.getAveragePriceSpecification = getAveragePriceSpecification;
            this.dateTimeConverter = dateTimeConverter;
        }

        public async override Task<IHandlerResult<AveragePriceDto>> Handle(
            GetAveragePriceQuery request,
            CancellationToken cancellationToken)
        {
            var startDate = dateTimeConverter.GetTimeSlotStartDate(request.Date);
            var filter = getAveragePriceSpecification.ToExpression(request);

            var prices = await priceRepository.GetAllAsync(filter, p => p.Value);

            if (prices.Any())
            {
                var result = AveragePriceDto.Create(startDate, prices.Average());
                return Data(result);
            }

            return NotFound();
        }
    }
}
