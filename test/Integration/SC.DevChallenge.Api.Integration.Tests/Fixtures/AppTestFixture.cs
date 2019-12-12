using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit.Abstractions;

namespace SC.DevChallenge.Api.Integration.Tests.Fixtures
{
    public class AppTestFixture : WebApplicationFactory<Startup>
    {
        public ITestOutputHelper? Output { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = base.CreateHostBuilder();
            return hostBuilder.UseSerilog(ConfigureSerilog);

            void ConfigureSerilog(HostBuilderContext hostBuilderContext, LoggerConfiguration configuration) =>
                configuration
                    .WriteTo.TestOutput(this.Output)
                    .MinimumLevel.Error();
        }
    }
}
