using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.Api.ExceptionHandling.Abstractions;

namespace SC.DevChallenge.Api.Infrastructure
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IExceptionRequestHandler exceptionRequestHandler;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IExceptionRequestHandler exceptionRequestHandler)
        {
            this.exceptionRequestHandler = exceptionRequestHandler;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch (Exception ex) when (!(ex is StackOverflowException))
            {
                await this.exceptionRequestHandler.Handle(httpContext, ex);
            }
        }
    }
}
