using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Controllers.Base;
using SC.DevChallenge.Dto;
using SC.DevChallenge.Mapping.Abstractions;
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

        /// <summary>
        /// Get Average Price fro specified PIIT
        /// </summary>
        /// <response code="200">Average price returned successfully</response>
        /// <response code="404">Specified PIIT not found</response>
        [HttpGet("average", Name = "GetAveragePrice")]
        [ProducesResponseType(typeof(AveragePriceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAveragePrice(
            [FromQuery] GetAveragePriceDto data)
        { 
            var query = Mapper.Map<GetAveragePriceQuery>(data);
            var result = await Mediator.Send(query);
            return Send(result);
        }
    }
}
