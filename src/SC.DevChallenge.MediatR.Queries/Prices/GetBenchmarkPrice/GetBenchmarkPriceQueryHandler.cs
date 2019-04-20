using System;
using System.Threading;
using System.Threading.Tasks;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceQueryHandler : QueryHandlerBase<GetBenchmarkPriceQuery, BenchmarkPriceDto>
    {
        public override Task<IHandlerResult<BenchmarkPriceDto>> Handle(GetBenchmarkPriceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
