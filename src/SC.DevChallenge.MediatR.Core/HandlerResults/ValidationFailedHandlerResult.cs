using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Core.HandlerResults
{
    public class ValidationFailedHandlerResult<T> : IHandlerResult<T>
    {
        public ValidationFailedHandlerResult(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
