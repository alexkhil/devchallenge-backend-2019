using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Controllers.Base;
using SC.DevChallenge.Dto;
using SC.DevChallenge.Dto.Prices.GetAggregatePrice;
using SC.DevChallenge.Dto.Prices.GetAveragePrice;
using SC.DevChallenge.Dto.Prices.GetBenchmarkPrice;
using SC.DevChallenge.Mapping.Abstractions;
using SC.DevChallenge.MediatR.Queries.Prices.GetAggregatePrice;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice;

namespace SC.DevChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    public class PricesController : ApiControllerBase
    {
        public PricesController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        /// <summary>
        /// Get Average Price fro specified PIIT
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Average price returned successfully</response>
        /// <response code="404">Specified PIIT not found</response>
        [HttpGet("average", Name = nameof(GetAveragePrice))]
        [ProducesResponseType(typeof(AveragePriceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAveragePrice(
            [FromQuery] GetAveragePriceDto data)
        {
            var query = Mapper.Map<GetAveragePriceQuery>(data);
            var result = await Mediator.Send(query);
            return Send(result);
        }

        /// <summary>
        /// Get Benchmark
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200"></response>
        /// <response code="404">Specified portfolio not found</response>
        [HttpGet("benchmark", Name = nameof(GetBenchmarkPrice))]
        [ProducesResponseType(typeof(BenchmarkPriceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBenchmarkPrice(
            [FromQuery] GetBenchmarkPriceDto data)
        {
            var query = Mapper.Map<GetBenchmarkPriceQuery>(data);
            var result = await Mediator.Send(query);
            return Send(result);
        }

        /// <summary>
        /// Get Benchmark
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200"></response>
        /// <response code="404">Specified PIIT not found</response>
        [HttpGet("aggregate", Name = nameof(GetAggregatePrice))]
        [ProducesResponseType(typeof(BenchmarkPriceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAggregatePrice(
            [FromQuery] GetAggregatePriceDto data)
        {
            var query = Mapper.Map<GetAggregatePriceQuery>(data);
            var result = await Mediator.Send(query);
            return Send(result);
        }
    }
}
