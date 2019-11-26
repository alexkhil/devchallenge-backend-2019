using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetAggregate
{
    public class GetAggregatePriceQueryHandler : QueryHandlerBase<GetAggregatePriceQuery, List<AggregatePriceViewModel>>
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

        public override async Task<IHandlerResult<List<AggregatePriceViewModel>>> Handle(
            GetAggregatePriceQuery request,
            CancellationToken cancellationToken)
        {
            var startTimeSlot = this.dateTimeConverter.DateTimeToTimeSlot(request.StartDate);
            var endTimeSlot = this.dateTimeConverter.DateTimeToTimeSlot(request.EndDate);

            var avgPriceByTimeslot = await this.priceRepository.GetAveragePricesAsync(request.Portfolio, startTimeSlot, endTimeSlot);
            if (!avgPriceByTimeslot.Any())
            {
                return NotFound();
            }

            var timeslots = endTimeSlot - startTimeSlot;
            var elementsInGroup = timeslots / request.ResultPoints;
            var remainder = timeslots % request.ResultPoints;
            var groupCounts = Enumerable.Range(1, request.ResultPoints).Select((x, i) => i + 1 <= remainder ? elementsInGroup + 1 : elementsInGroup).ToList();

            var result = new List<AggregatePriceViewModel>(request.ResultPoints);
            var start = startTimeSlot;
            foreach (var num in groupCounts)
            {
                var end = start + num - 1;
                result.Add(new AggregatePriceViewModel
                {
                    Date = this.dateTimeConverter.GetTimeSlotStartDate(end),
                    Price = Math.Round(avgPriceByTimeslot.Where(x => x.Key >= start && x.Key <= end).Average(x => x.Value), 2)
                });
                start = end + 1;
            }

            return Data(result);
        }
    }
}
