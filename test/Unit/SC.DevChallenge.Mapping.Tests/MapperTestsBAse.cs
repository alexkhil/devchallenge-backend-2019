using SC.DevChallenge.Mapping.Abstractions;
using SC.DevChallenge.Mapping.MappingProfiles;

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

        protected static AutoMapper.MapperConfiguration MapperConfig { get; } = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<GetAveragePriceProfile>());
    }
}
