using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.Prices.GetAverage.Specifications;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetAverage
{
    public class GetAveragePriceQueryHandler : QueryHandlerBase<GetAveragePriceQuery, AveragePriceViewModel>
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

        public override async Task<IHandlerResult<AveragePriceViewModel>> Handle(
            GetAveragePriceQuery request,
            CancellationToken cancellationToken)
        {
            var startDate = this.dateTimeConverter.GetTimeSlotStartDate(request.Date);
            var filter = this.getAveragePriceSpecification.ToExpression(request);

            var prices = await this.priceRepository.GetAllAsync(filter, p => p.Value);

            if (prices == null || !prices.Any())
            {
                return NotFound();
            }

            var result = new AveragePriceViewModel { Date = startDate, Price = prices.Average() };
            return Data(result);
        }
    }
}
