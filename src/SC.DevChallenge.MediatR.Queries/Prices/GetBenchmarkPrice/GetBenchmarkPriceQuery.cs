using System;
using MediatR;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceQuery
        : IRequest<IHandlerResult<BenchmarkPriceDto>>
    {
        public string Portfolio { get; set; }

        public DateTime Date { get; set; }
    }
}
