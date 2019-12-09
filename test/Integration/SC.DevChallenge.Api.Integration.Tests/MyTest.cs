using Autofac;
using MediatR;
using SC.DevChallenge.Api.IoC;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;
using SC.DevChallenge.Queries.Prices.GetBenchmark;
using SC.DevChallenge.Queries.ViewModels;
using Xunit;

namespace SC.DevChallenge.Api.Integration.Tests
{
    public class MyTest
    {
        private IContainer container;

        public MyTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<QueriesModule>();
            this.container = builder.Build();
        }

        [Fact]
        public void MyTestMethod()
        {
            var actual = this.container.IsRegistered(typeof(IRequestHandler<GetBenchmarkPriceQuery, IHandlerResult<BenchmarkPriceViewModel>>));
            Assert.True(actual);
        }
    }
}
