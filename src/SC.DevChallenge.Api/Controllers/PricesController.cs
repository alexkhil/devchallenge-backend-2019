using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Requests.Prices;
using SC.DevChallenge.Queries.Prices.GetAggregate;
using SC.DevChallenge.Queries.Prices.GetAverage;
using SC.DevChallenge.Queries.Prices.GetBenchmark;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    public class PricesController : ApiController
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
        [HttpGet("average", Name = nameof(GetAverage))]
        [ProducesResponseType(typeof(AveragePriceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAverage(
            [FromQuery] GetAveragePriceDto data)
        {
            var query = this.Mapper.Map<GetAveragePriceQuery>(data);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }

        /// <summary>
        /// Get Benchmark
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200"></response>
        /// <response code="404">Specified portfolio not found</response>
        [HttpGet("benchmark", Name = nameof(GetBenchmark))]
        [ProducesResponseType(typeof(BenchmarkPriceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBenchmark(
            [FromQuery] GetBenchmarkPriceDto data)
        {
            var query = this.Mapper.Map<GetBenchmarkPriceQuery>(data);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }

        /// <summary>
        /// Get Benchmark
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200"></response>
        /// <response code="404">Specified PIIT not found</response>
        [HttpGet("aggregate", Name = nameof(GetAggregate))]
        [ProducesResponseType(typeof(BenchmarkPriceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAggregate(
            [FromQuery] GetAggregatePriceDto data)
        {
            var query = this.Mapper.Map<GetAggregatePriceQuery>(data);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }
    }
}
