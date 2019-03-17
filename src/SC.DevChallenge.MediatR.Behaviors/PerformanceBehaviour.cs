using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SC.DevChallenge.MediatR.Behaviors
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch timer;
        private readonly ILogger<TRequest> logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            timer = new Stopwatch();
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            timer.Start();

            var response = await next();

            timer.Stop();

            if (timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                logger.LogWarning($"Long Running Request: {name} ({timer.ElapsedMilliseconds} milliseconds) {request}");
            }

            return response;
        }
    }
}
