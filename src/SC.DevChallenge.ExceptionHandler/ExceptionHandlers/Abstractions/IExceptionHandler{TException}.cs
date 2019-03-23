using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SC.DevChallenge.ExceptionHandler
{
    public interface IExceptionHandler<in TException> : IExceptionHandler
        where TException : Exception
    {
        Task HandleException(TException exception, HttpContext context);
    }
}
