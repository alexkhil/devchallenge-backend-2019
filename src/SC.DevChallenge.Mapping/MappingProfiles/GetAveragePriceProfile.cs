using AutoMapper;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;

namespace SC.DevChallenge.Mapping.MappingProfiles
{
    public class GetAveragePriceProfile : Profile
    {
        public GetAveragePriceProfile()
        {
            CreateMap<GetAveragePriceDto, GetAveragePriceQuery>();
        }
    }
}
