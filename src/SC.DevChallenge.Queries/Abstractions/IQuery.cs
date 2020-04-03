using MediatR;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.Queries.Abstractions
{
    public interface IQuery<T> : IRequest<IHandlerResult<T>>
    {
    }
}
