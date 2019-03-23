using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Mapping.Abstractions;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.Api.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

        public IMapper Mapper { get; }

        public IMediator Mediator { get; }

        protected IActionResult Send<T>(IHandlerResult<T> result)
            where T : class
        {
            return RemapInternal(result);
        }

        private IActionResult RemapInternal<T>(IHandlerResult<T> result)
            where T : class
        {
            switch (result)
            {
                case DataHandlerResult<T> dhr:
                    return Ok(dhr.Data);
                default:
                    return NotFound();
            }
        }
    }
}
