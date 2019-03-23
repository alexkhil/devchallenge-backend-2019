using System;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Core.HandlerResults
{
    public class DataHandlerResult<T> : IHandlerResult<T>
        where T : class
    {
        public DataHandlerResult(T data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public T Data { get; }
    }
}
