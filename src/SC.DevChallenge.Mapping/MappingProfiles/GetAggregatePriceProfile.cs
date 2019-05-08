using AutoMapper;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice;

namespace SC.DevChallenge.Mapping.MappingProfiles
{
    public class GetAggregatePriceProfile : Profile
    {
        public GetAggregatePriceProfile()
        {
            CreateMap<GetAggregatePriceDto, GetAggregatePriceQuery>();
        }
    }
}
