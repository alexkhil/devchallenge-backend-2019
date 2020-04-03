using System;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetBenchmark
{
    public sealed class GetBenchmarkPriceQuery : IQuery<BenchmarkPriceViewModel>
    {
        public string Portfolio { get; set; }

        public DateTime Date { get; set; }
    }
}
