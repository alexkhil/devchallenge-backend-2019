using System;
using System.Net.Http;
using SC.DevChallenge.Api.Integration.Tests.Fixtures;
using SC.DevChallenge.Api.Integration.Tests.Fixtures.Collections;
using Xunit;
using Xunit.Abstractions;

namespace SC.DevChallenge.Api.Integration.Tests.Controllers
{
    [Collection(nameof(DevChallengeServerCollection))]
    public class BaseControllerTest : IDisposable
    {
        private readonly AppTestFixture webAppFactory;
        private bool disposedValue;

        protected BaseControllerTest(AppTestFixture webAppFactory, ITestOutputHelper output)
        {
            this.webAppFactory = webAppFactory;
            this.webAppFactory.Output = output;
            this.Client = this.webAppFactory.CreateClient();
        }

        protected HttpClient Client { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue)
            {
                return;
            }

            if (disposing)
            {
                this.webAppFactory.Output = null;
            }

            this.disposedValue = true;
        }
    }
}
