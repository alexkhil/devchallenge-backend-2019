using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SC.DevChallenge.Api.ExceptionHandling.Abstractions
{
    public interface IExceptionHandler
    {
        Task HandleException(Exception exception, HttpContext context);
    }
}
