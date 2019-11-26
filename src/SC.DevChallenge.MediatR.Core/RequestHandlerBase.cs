using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Core
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, IHandlerResult<TResponse>>
        where TRequest : IRequest<IHandlerResult<TResponse>>
    {
        public abstract Task<IHandlerResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

        protected static IHandlerResult<TResponse> NotFound() =>
            new NotFoundHandlerResult<TResponse>();

        protected static IHandlerResult<TResponse> Data(TResponse data) =>
            new DataHandlerResult<TResponse>(data);

        protected static IHandlerResult<TResponse> ValidationFailed(string message) =>
            new ValidationFailedHandlerResult<TResponse>(message);
    }
}
