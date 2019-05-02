using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SC.DevChallenge.ExceptionHandler.Abstractions;

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
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await exceptionRequestHandler.Handle(httpContext, ex);
            }
        }
    }
}
