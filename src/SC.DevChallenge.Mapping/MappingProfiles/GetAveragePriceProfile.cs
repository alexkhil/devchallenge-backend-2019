using System;
using System.Globalization;
using AutoMapper;
using SC.DevChallenge.Domain.Date;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;

namespace SC.DevChallenge.Mapping.MappingProfiles
{
    public class GetAveragePriceProfile : Profile
    {
        public GetAveragePriceProfile()
        {
            CreateMap<GetAveragePriceDto, GetAveragePriceQuery>()
                .ForMember(query => query.Date, dto => dto.MapFrom(d => DateTime.ParseExact(d.Date, DateTimeFormat.Default, CultureInfo.InvariantCulture)));
        }
    }
}
