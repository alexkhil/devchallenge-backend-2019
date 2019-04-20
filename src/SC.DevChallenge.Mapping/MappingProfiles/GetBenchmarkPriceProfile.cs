using System;
using System.Globalization;
using AutoMapper;
using SC.DevChallenge.Domain.Date;
using SC.DevChallenge.Dto.Prices.GetBenchmarkPrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice;

namespace SC.DevChallenge.Mapping.MappingProfiles
{
    public class GetBenchmarkPriceProfile : Profile
    {
        public GetBenchmarkPriceProfile()
        {
            CreateMap<GetBenchmarkPriceDto, GetBenchmarkPriceQuery>()
                .ForMember(query => query.Date, dto => dto.MapFrom(d => DateTime.ParseExact(d.Date, DateTimeFormat.Default, CultureInfo.InvariantCulture)));
        }
    }
}
