using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.MediatR.Core.HandlerResults;
using SC.DevChallenge.MediatR.Core.HandlerResults.Abstractions;

namespace SC.DevChallenge.Api.Controllers
{
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        public IMapper Mapper { get; set; }

        public IMediator Mediator { get; set; }

        protected IActionResult FromResult<T>(IHandlerResult<T> result) where T : class =>
            result switch
            {
                DataHandlerResult<T> dhr => this.Ok(dhr.Data),
                _ => this.NotFound(),
            };
    }
}
