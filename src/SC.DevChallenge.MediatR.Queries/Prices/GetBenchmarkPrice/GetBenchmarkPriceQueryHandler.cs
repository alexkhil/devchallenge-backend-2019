using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
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

        public GetBenchmarkPriceQueryHandler(
            IGetBenchmarkPriceSpecification specification,
            IPriceRepository priceRepository,
            IDateTimeConverter dateTimeConverter)
        {
            this.specification = specification;
            this.priceRepository = priceRepository;
            this.dateTimeConverter = dateTimeConverter;
        }

        public async override Task<IHandlerResult<BenchmarkPriceDto>> Handle(
            GetBenchmarkPriceQuery request,
            CancellationToken cancellationToken)
        {
            var pavs = await priceRepository.GetPiceAveragePricesAsync();

            var filter = specification.ToExpression(request);
            var prices = await priceRepository.GetAllAsync(filter);

            var timeslot = dateTimeConverter.DateTimeToTimeSlot(request.Date);
            var pricesCount = await priceRepository.GetPricesCount(timeslot);

            var q1 = GetQuarter(1, pricesCount);
            var q3 = GetQuarter(3, pricesCount);

            var iqr = pavs[q3] - pavs[q1];

            var lowerBound = pavs[q1] - 1.5 * iqr;
            var higherBound = pavs[q3] + 1.5 * iqr;

            var averagePrice = prices.Where(p => p.Value > lowerBound && p.Value < higherBound).Average(p => p.Value);

            var startDate = dateTimeConverter.GetTimeSlotStartDate(timeslot);
            var benchmarkResult = BenchmarkPriceDto.Create(startDate, averagePrice);

            return Data(benchmarkResult);
        }

        private static int GetQuarter(int quarterNum, int elementNum) =>
            (int)Math.Ceiling(((quarterNum * elementNum) - quarterNum) / 4.0);
    }
}
