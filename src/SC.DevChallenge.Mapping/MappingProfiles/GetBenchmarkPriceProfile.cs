using AutoMapper;
using SC.DevChallenge.Dto.Prices.GetBenchmarkPrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice;

namespace SC.DevChallenge.Mapping.MappingProfiles
{
    public class GetBenchmarkPriceProfile : Profile
    {
        public GetBenchmarkPriceProfile()
        {
            CreateMap<GetBenchmarkPriceDto, GetBenchmarkPriceQuery>();
        }
    }
}
