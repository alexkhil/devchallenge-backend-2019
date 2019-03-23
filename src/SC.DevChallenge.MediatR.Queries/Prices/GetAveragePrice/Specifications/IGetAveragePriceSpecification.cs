using System;
using System.Linq.Expressions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications
{
    public interface IGetAveragePriceSpecification
    {
        bool IsSatisfiedBy(Price price, string portfolio, string owner, string instrument, int timeslot);
        Expression<Func<Price, bool>> ToExpression(string portfolio, string owner, string instrument, int timeslot);
    }
}