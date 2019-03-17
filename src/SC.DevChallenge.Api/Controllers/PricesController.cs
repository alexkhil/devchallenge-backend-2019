using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Controllers.Base;
using SC.DevChallenge.Dto;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;

namespace SC.DevChallenge.Api.Controllers
{
    [Route("api/prices")]
    public class PricesController : ApiControllerBase
    {
        public PricesController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet("average")]
        public async Task<ActionResult<AveragePriceDto>> GetAveragePrice(
            [FromQuery] GetAveragePriceQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
