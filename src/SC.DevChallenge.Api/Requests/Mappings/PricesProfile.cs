using AutoMapper;
using SC.DevChallenge.Api.Requests.Prices;
using SC.DevChallenge.Queries.Prices.GetAggregate;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetBenchmark;

namespace SC.DevChallenge.Api.Requests.Mappings
{
    public class PricesProfile : Profile
    {
        public PricesProfile()
        {
            this.CreateMap<GetAggregatePriceDto, GetAggregatePriceQuery>();
            this.CreateMap<GetAveragePriceDto, GetAveragePriceQuery>();
            this.CreateMap<GetBenchmarkPriceDto, GetBenchmarkPriceQuery>();
        }
    }
}
