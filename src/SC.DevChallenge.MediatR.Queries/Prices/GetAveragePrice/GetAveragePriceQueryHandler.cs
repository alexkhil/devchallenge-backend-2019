using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice
{
    public class GetAveragePriceQueryHandler : IRequestHandler<GetAveragePriceQuery, AveragePriceDto>
    {
        public Task<AveragePriceDto> Handle(GetAveragePriceQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
