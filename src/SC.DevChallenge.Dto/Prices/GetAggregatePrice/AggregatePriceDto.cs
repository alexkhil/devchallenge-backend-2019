using System;
using System.Globalization;

namespace SC.DevChallenge.Dto.Prices.GetAggregatePrice
{
    public class AggregatePriceDto
    {
        protected AggregatePriceDto()
        {
        }

        /// <summary>
        /// The timeslot start date
        /// </summary>
        /// <example>01/01/2018 12:15:30</example>
        public string Date { get; private set; }

        /// <summary>
        /// The average price
        /// </summary>
        /// <example>4.00</example>
        public double Price { get; private set; }

        public static AggregatePriceDto Create(DateTime startDate, double averagePrice)
        {
            return new AggregatePriceDto
            {
                Date = startDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Price = Math.Round(averagePrice, 2)
            };
        }
    }
}
