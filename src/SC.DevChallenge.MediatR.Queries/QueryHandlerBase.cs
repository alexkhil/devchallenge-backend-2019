using MediatR;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Queries
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
        where TRequest : IRequest<IHandlerResult<TResponse>>
    {
    }
}
