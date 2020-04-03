using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.Api.ExceptionHandling.Abstractions;

namespace SC.DevChallenge.Api.ExceptionHandling
{
    public class ExceptionRequestHandler : IExceptionRequestHandler
    {
        private static readonly Type handlerOpenType = typeof(IExceptionHandler<>);

        private readonly ILogger<IExceptionHandler> logger;

        public ExceptionRequestHandler(ILogger<IExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(HttpContext context, Exception exception)
        {
            this.logger.LogError(exception, exception.Message);

            Type[] typeArgs = { exception.GetType() };

            var handler = context.RequestServices?.GetService(handlerOpenType.MakeGenericType(typeArgs))
                ?? context.RequestServices?.GetService<IExceptionHandler<Exception>>();

            if (handler == null)
            {
                this.logger.LogError("Can't resolve exception handler for {type}", exception.GetType());
                return;
            }

            await ((IExceptionHandler)handler).HandleException(exception, context);
        }
    }
}
