using System;
using System.Linq.Expressions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Date.DateTimeConverter;
using SC.DevChallenge.Specification;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetBenchmarkPrice.Specifications
{
    public class GetBenchmarkPriceSpecification :
        SpecificationBase<Price, GetBenchmarkPriceQuery>,
        IGetBenchmarkPriceSpecification
    {
        private readonly IDateTimeConverter dateTimeConverter;

        public GetBenchmarkPriceSpecification(
            IDateTimeConverter dateTimeConverter)
        {
            this.dateTimeConverter = dateTimeConverter;
        }

        public override Expression<Func<Price, bool>> ToExpression(
            GetBenchmarkPriceQuery request)
        {
            var timeslot = dateTimeConverter.DateTimeToTimeSlot(request.Date);

            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == request.Portfolio &&
                x.Timeslot == timeslot;

            return filter;
        }
    }
}
