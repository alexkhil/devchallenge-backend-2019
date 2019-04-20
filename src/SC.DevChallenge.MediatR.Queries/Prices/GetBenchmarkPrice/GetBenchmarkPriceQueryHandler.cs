using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
using SC.DevChallenge.Domain.Quarter;
using SC.DevChallenge.Domain.Timeslot;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice.Specifications;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceQueryHandler : QueryHandlerBase<GetBenchmarkPriceQuery, BenchmarkPriceDto>
    {
        private readonly IGetBenchmarkPriceSpecification specification;
        private readonly IPriceRepository priceRepository;
        private readonly IDateTimeConverter dateTimeConverter;
        private readonly IQuarterCalculator quarterCalculator;
        private readonly ITimeslotCalculator timeslotCalculator;

        public GetBenchmarkPriceQueryHandler(
            IGetBenchmarkPriceSpecification specification,
            IPriceRepository priceRepository,
            IDateTimeConverter dateTimeConverter,
            IQuarterCalculator quarterCalculator,
            ITimeslotCalculator timeslotCalculator)
        {
            this.specification = specification;
            this.priceRepository = priceRepository;
            this.dateTimeConverter = dateTimeConverter;
            this.quarterCalculator = quarterCalculator;
            this.timeslotCalculator = timeslotCalculator;
        }

        public async override Task<IHandlerResult<BenchmarkPriceDto>> Handle(
            GetBenchmarkPriceQuery request,
            CancellationToken cancellationToken)
        {
            var pavs = await priceRepository.GetPiceAveragePricesAsync();

            var filter = specification.ToExpression(request);
            var prices = await priceRepository.GetAllAsync(filter);

            if (!prices.Any())
            {
                return NotFound();
            }

            var timeslot = dateTimeConverter.DateTimeToTimeSlot(request.Date);
            var timeslotPricesCount = await priceRepository.GetPricesCount(timeslot);

            var firstQuarter = quarterCalculator.GetQuarter(1, timeslotPricesCount);
            var thirdQuarter = quarterCalculator.GetQuarter(3, timeslotPricesCount);

            var interQuartileRange = pavs[thirdQuarter] - pavs[firstQuarter];

            var lowerBound = timeslotCalculator.GetLowerBound(pavs[firstQuarter], interQuartileRange);
            var higherBound = timeslotCalculator.GetHigherBound(pavs[firstQuarter], interQuartileRange);

            var averagePrice = prices.Where(p => p.Value > lowerBound && p.Value < higherBound).Average(p => p.Value);

            var startDate = dateTimeConverter.GetTimeSlotStartDate(timeslot);
            var benchmarkResult = BenchmarkPriceDto.Create(startDate, averagePrice);

            return Data(benchmarkResult);
        }
    }
}
