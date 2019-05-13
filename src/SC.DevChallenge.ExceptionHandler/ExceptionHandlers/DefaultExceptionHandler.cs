using System;
using System.Net;
using SC.DevChallenge.ExceptionHandler.Abstractions;

namespace SC.DevChallenge.ExceptionHandler.ExceptionHandlers
{
    public class DefaultExceptionHandler : BaseExceptionHandler, IExceptionHandler<Exception>
    {
        private const string ErrorMessage = "Some unexpected error occurred.";

        protected override ErrorResponse CreateErrorMessage(Exception exception) =>
            new ErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage);
    }
}
