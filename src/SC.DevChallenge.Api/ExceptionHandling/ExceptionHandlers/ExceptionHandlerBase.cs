using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SC.DevChallenge.Api.ExceptionHandling.Abstractions;

namespace SC.DevChallenge.Api.ExceptionHandling.ExceptionHandlers
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        public Task HandleException(Exception exception, HttpContext context)
        {
            var errorResponse = this.CreateErrorMessage(exception);
            context.Response.StatusCode = (int)errorResponse.StatusCode;
            return context.Response.WriteAsync(errorResponse.Message);
        }

        protected abstract ErrorResponse CreateErrorMessage(Exception exception);

        protected class ErrorResponse
        {
            public ErrorResponse(HttpStatusCode statusCode, string message)
            {
                this.StatusCode = statusCode;
                this.Message = message;
            }

            public HttpStatusCode StatusCode { get; set; }

            public string Message { get; set; }
        }
    }
}
