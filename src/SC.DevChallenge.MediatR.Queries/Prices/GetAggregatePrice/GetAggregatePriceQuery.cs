using System;
using MediatR;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice
{
    public class GetAggregatePriceQuery : IRequest<IHandlerResult<AggregatePriceDto>>
    {
        public string Portfolio { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ResultPoints { get; set; }
    }
}
