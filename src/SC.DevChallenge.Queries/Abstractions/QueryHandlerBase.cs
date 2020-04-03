using SC.DevChallenge.MediatR.Core;

namespace SC.DevChallenge.Queries.Abstractions
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}
