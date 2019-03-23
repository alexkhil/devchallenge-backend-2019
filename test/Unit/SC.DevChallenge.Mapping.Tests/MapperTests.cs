using Xunit;

namespace SC.DevChallenge.Mapping.Tests
{
    public class MapperTests : MapperTestsBase
    {
        [Fact]
        public void Mapper_Configuration_IsValid()
        {
            MapperConfig.AssertConfigurationIsValid();
        }
    }
}
