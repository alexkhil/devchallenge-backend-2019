using AutoMapper;
using SC.DevChallenge.Mapping.MappingProfiles;
using IMapper = SC.DevChallenge.Mapping.Abstractions.IMapper;

namespace SC.DevChallenge.Mapping.Tests
{
    public abstract class MapperTestsBase
    {
        protected MapperTestsBase()
        {
            var mapper = MapperConfig.CreateMapper();
            Mapper = new Mapper(mapper);
        }

        protected IMapper Mapper { get; }

        protected static MapperConfiguration MapperConfig { get; } = new MapperConfiguration(cfg => cfg.AddProfile<GetAveragePriceProfile>());
    }
}
