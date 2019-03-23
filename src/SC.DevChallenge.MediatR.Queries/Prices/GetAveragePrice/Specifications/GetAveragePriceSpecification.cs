using System;
using System.Linq.Expressions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications
{
    public class GetAveragePriceSpecification : IGetAveragePriceSpecification
    {
        public Expression<Func<Price, bool>> ToExpression(
            string portfolio,
            string owner,
            string instrument,
            int timeslot)
        {
            Expression<Func<Price, bool>> priceFilter = p => p.Portfolio.Name == portfolio
                && p.Owner.Name == owner
                && p.Instrument.Name == instrument
                && p.Timeslot == timeslot;

            return priceFilter;
        }

        public bool IsSatisfiedBy(
            Price price,
            string portfolio,
            string owner,
            string instrument,
            int timeslot)
        {
            Func<Price, bool> predicate = ToExpression(portfolio, owner, instrument, timeslot).Compile(preferInterpretation: true);

            return predicate.Invoke(price);
        }
    }
}
