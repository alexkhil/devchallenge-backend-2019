using Autofac;
using SC.DevChallenge.Api.Controllers;
using SC.DevChallenge.Api.IoC;
using Xunit;

namespace SC.DevChallenge.Api.Tests
{
    public class ModuleTest
    {
        private readonly IContainer container;

        public ModuleTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MappingModule>().RegisterModule<ApiModule>();

            this.container = builder.Build();
        }

        [Fact]
        public void Given_When_Than()
        {
            // Arrange

            // Act
            var actual = container.Resolve<PricesController>();
            // Assert
            Assert.NotNull(actual);
        }
    }
}
