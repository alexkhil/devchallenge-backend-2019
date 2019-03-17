using SC.DevChallenge.Mapping.Abstractions;

namespace SC.DevChallenge.Mapping
{
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}