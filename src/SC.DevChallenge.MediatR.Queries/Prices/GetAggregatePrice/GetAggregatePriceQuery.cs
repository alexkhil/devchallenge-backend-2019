using System;
using MediatR;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQuery : IRequest<IHandlerResult<AggregatePriceDto>>
    {
        public string Portfolio { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int NumberOfAggregatedPrices { get; set; }
    }
}
