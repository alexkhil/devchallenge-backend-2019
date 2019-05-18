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
            webAppFactory = new WebApplicationFactory<Startup>();
            Client = webAppFactory.CreateClient();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    webAppFactory?.Dispose();
                    Client?.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}
