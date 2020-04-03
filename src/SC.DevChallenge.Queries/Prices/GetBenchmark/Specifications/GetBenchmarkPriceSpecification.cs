using System;
using System.Linq.Expressions;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Queries.Abstractions;

namespace SC.DevChallenge.Queries.Prices.GetBenchmark.Specifications
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
            var timeslot = this.dateTimeConverter.DateTimeToTimeSlot(request.Date);

            Expression<Func<Price, bool>> filter = x =>
                x.Portfolio.Name == request.Portfolio &&
                x.Timeslot == timeslot;

            return filter;
        }
    }
}
