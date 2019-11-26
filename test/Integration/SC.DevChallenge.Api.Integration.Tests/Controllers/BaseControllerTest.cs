using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SC.DevChallenge.Api.Integration.Tests.Controllers
{
    public class BaseControllerTest : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webAppFactory;
        private bool disposedValue;

        protected BaseControllerTest()
        {
            this.webAppFactory = new WebApplicationFactory<Startup>();
            this.Client = this.webAppFactory.CreateClient();
        }

        public HttpClient Client { get; }

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
                this.webAppFactory?.Dispose();
                this.Client?.Dispose();
            }

            this.disposedValue = true;
        }
    }
}
