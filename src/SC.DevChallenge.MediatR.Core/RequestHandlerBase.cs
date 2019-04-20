using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, IHandlerResult<TResponse>>
        where TRequest : IRequest<IHandlerResult<TResponse>>
    {
        public abstract Task<IHandlerResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

        protected IHandlerResult<TResponse> NotFound()
        {
            return new NotFoundHandlerResult<TResponse>();
        }

        protected IHandlerResult<TResponse> Data(TResponse data)
        {
            return new DataHandlerResult<TResponse>(data);
        }
    }
}
