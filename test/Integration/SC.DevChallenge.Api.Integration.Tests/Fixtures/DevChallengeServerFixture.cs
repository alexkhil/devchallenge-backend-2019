using System.Net.Http;
using System.Threading.Tasks;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;
using Xunit;

namespace SC.DevChallenge.Api.Integration.Tests.Fixtures
{
    public class DevChallengeServerFixture : IAsyncLifetime
    {
        private readonly IDockerContainer sqlServerContainer;
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        public DevChallengeServerFixture()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.sqlServerContainer = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "Your_password123")
                .WithPortBinding(1433)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
                .Build();
        }

        public HttpClient HttpClient { get; private set; }
        
        public async Task InitializeAsync()
        {
            await this.sqlServerContainer.StartAsync();
            await this.webApplicationFactory.Services
                .GetService<IDbInitializer>()
                .InitializeAsync();

            this.HttpClient = this.webApplicationFactory.CreateClient();
        }

        public Task DisposeAsync() => this.sqlServerContainer.DisposeAsync().AsTask();
    }
}