using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SC.DevChallenge.Api.ExceptionHandling.Abstractions
{
    public interface IExceptionRequestHandler
    {
        Task Handle(HttpContext context, Exception exception);
    }
}
