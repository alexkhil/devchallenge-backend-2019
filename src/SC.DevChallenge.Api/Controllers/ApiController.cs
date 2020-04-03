using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiController : ControllerBase
    {
        protected ApiController(IMapper mapper, IMediator mediator) =>
            (this.Mapper, this.Mediator) = (mapper, mediator);

        protected IMapper Mapper { get; }

        protected IMediator Mediator { get; }

        protected IActionResult FromResult<T>(IHandlerResult<T> result) where T : class =>
            result switch
            {
                DataHandlerResult<T> dhr => this.Ok(dhr.Data) as IActionResult,
                _ => this.NotFound() as IActionResult,
            };
    }
}
