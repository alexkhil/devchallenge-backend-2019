using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.DateTimeConverter;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQueryHandler : QueryHandlerBase<GetAggregatePriceQuery, List<AggregatePriceDto>>
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

        public override async Task<IHandlerResult<List<AggregatePriceDto>>> Handle(
            GetAggregatePriceQuery request,
            CancellationToken cancellationToken)
        {
            var startTimeSlot = dateTimeConverter.DateTimeToTimeSlot(request.StartDate);
            var endTimeSlot = dateTimeConverter.DateTimeToTimeSlot(request.EndDate);

            var avgPriceByTimeslot = await priceRepository.GetAveragePricesAsync(request.Portfolio, startTimeSlot, endTimeSlot);
            if (!avgPriceByTimeslot.Any())
            {
                return NotFound();
            }

            var timeslots = endTimeSlot - startTimeSlot;
            var elementsInGroup = (timeslots / request.ResultPoints);
            var remainder = timeslots % request.ResultPoints;
            var groupCounts = Enumerable.Range(1, request.ResultPoints).Select((x, i) => i + 1 <= remainder ? elementsInGroup + 1 : elementsInGroup).ToList();

            var result = new List<AggregatePriceDto>(request.ResultPoints);
            var start = startTimeSlot;
            foreach (var num in groupCounts)
            {
                var end = start + num - 1;
                result.Add(new AggregatePriceDto
                {
                    Date = dateTimeConverter.GetTimeSlotStartDate(end),
                    Price = Math.Round(avgPriceByTimeslot.Where(x => x.Key >= start && x.Key <= end).Average(x => x.Value), 2)
                });
                start = end + 1;
            }

            return Data(result);
        }
    }
}
