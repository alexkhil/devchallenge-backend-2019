using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
