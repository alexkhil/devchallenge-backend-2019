using System;
using System.Linq.Expressions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
using SC.DevChallenge.MediatR.Queries.Prices.GetAverage;
using SC.DevChallenge.Specification;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAveragePrice.Specifications
{
    public class GetAveragePriceSpecification :
        SpecificationBase<Price, GetAveragePriceQuery>,
        IGetAveragePriceSpecification
    {
        private readonly IDateTimeConverter dateTimeConverter;

        public GetAveragePriceSpecification(
            IDateTimeConverter dateTimeConverter)
        {
            this.dateTimeConverter = dateTimeConverter;
        }

        public override Expression<Func<Price, bool>> ToExpression(
            GetAveragePriceQuery request)
        {
            var timeslot = dateTimeConverter.DateTimeToTimeSlot(request.Date);

            Expression<Func<Price, bool>> priceFilter = p => p.Portfolio.Name == request.Portfolio
                && p.Owner.Name == request.Owner
                && p.Instrument.Name == request.Instrument
                && p.Timeslot == timeslot;

            return priceFilter;
        }
    }
}
