using System;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.MediatR.Core.HandlerResults
{
    public class DataHandlerResult<T> : IHandlerResult<T>
    {
        public DataHandlerResult(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
