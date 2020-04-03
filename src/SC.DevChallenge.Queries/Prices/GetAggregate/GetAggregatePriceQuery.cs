using System;
using System.Collections.Generic;
using SC.DevChallenge.Queries.Abstractions;
using SC.DevChallenge.Queries.ViewModels;

namespace SC.DevChallenge.Queries.Prices.GetAggregate
{
    public sealed class GetAggregatePriceQuery : IQuery<List<AggregatePriceViewModel>>
    {
        public string Portfolio { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ResultPoints { get; set; }
    }
}
