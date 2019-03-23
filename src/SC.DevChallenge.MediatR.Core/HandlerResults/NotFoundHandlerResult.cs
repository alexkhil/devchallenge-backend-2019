using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Core.HandlerResults
{
    public class NotFoundHandlerResult<T> : IHandlerResult<T>
        where T : class
    {
    }
}
