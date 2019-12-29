using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.Api.Controllers
{
    [Produces("application/json")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMapper mapper;
        protected readonly IMediator mediator;

        protected ApiController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        protected IActionResult FromResult<T>(IHandlerResult<T> result) where T : class =>
            result switch
            {
                DataHandlerResult<T> dhr => this.Ok(dhr.Data) as IActionResult,
                _ => this.NotFound() as IActionResult,
            };
    }
}
