using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.Prices.GetBenchmark.Specifications;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetBenchmark
{
    public class GetBenchmarkPriceQueryHandler : QueryHandlerBase<GetBenchmarkPriceQuery, BenchmarkPriceViewModel>
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

        public override async Task<IHandlerResult<BenchmarkPriceViewModel>> Handle(
            GetBenchmarkPriceQuery request,
            CancellationToken cancellationToken)
        {
            var filter = this.specification.ToExpression(request);
            var prices = await this.priceRepository.GetAllAsync(filter);

            if (prices == null || !prices.Any())
            {
                return NotFound();
            }

            var timeslot = this.dateTimeConverter.DateTimeToTimeSlot(request.Date);
            var timeslotPricesCount = await this.priceRepository.GetPricesCount(timeslot);

            var firstQuarter = this.quarterCalculator.GetQuarter(1, timeslotPricesCount);
            var thirdQuarter = this.quarterCalculator.GetQuarter(3, timeslotPricesCount);

            var pavs = await this.priceRepository.GetAveragePricesAsync();
            if (!IsQuarterExist(pavs, firstQuarter) ||
                !IsQuarterExist(pavs, thirdQuarter))
            {
                return ValidationFailed("Invalid quarter out of range");
            }

            var interQuartileRange = pavs[thirdQuarter] - pavs[firstQuarter];

            var lowerBound = this.timeslotCalculator.GetLowerBound(pavs[firstQuarter], interQuartileRange);
            var higherBound = this.timeslotCalculator.GetHigherBound(pavs[firstQuarter], interQuartileRange);

            var averagePrice = prices.Where(p => p.Value > lowerBound && p.Value < higherBound).Average(p => p.Value);

            var startDate = this.dateTimeConverter.GetTimeSlotStartDate(timeslot);

            var benchmarkResult = new BenchmarkPriceViewModel
            {
                Date = startDate,
                Price = averagePrice
            };

            return Data(benchmarkResult);
        }

        private static bool IsQuarterExist(ICollection<double> list, int quarter) =>
            quarter >= 0 && quarter < list.Count;
    }
}
