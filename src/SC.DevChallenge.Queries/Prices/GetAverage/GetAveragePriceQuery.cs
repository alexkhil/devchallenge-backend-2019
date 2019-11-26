using System;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetAverage
{
    public sealed class GetAveragePriceQuery : IQuery<AveragePriceViewModel>
    {
        public string Portfolio { get; set; }

        public string Owner { get; set; }

        public string Instrument { get; set; }

        public DateTime Date { get; set; }
    }
}
