using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, IHandlerResult<TResponse>>
    {
        private readonly ILogger<TRequest> logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<IHandlerResult<TResponse>> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<IHandlerResult<TResponse>> next)
        {
            using (logger.BeginScope("Handling {Request} request.", typeof(TRequest).Name))
            {
                try
                {
                    logger.LogInformation("Request {Request} is obtained.", typeof(TRequest).Name);

                    var response = await next();

                    logger.LogInformation("Response {Response} is obtained.", typeof(TResponse).Name);

                    return response;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    throw;
                }
            }
        }
    }
}
