using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SC.DevChallenge.MediatR.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> logger;
        private readonly Stopwatch timer;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
            timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            using (logger.BeginScope("Handling {Request} request.", request))
            {
                timer.Restart();
                var response = await next();
                timer.Stop();

                logger.LogInformation("Response {Response} is obtained in {Time} ms.", typeof(TRequest).Name, timer.ElapsedMilliseconds);

                return response;
            }
        }
    }
}
