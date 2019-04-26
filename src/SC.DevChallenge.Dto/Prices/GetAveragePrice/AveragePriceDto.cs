using System;

namespace SC.DevChallenge.Dto.Prices.GetAveragePrice
{
    public class AveragePriceDto
    {
        protected AveragePriceDto()
        {
        }

        /// <summary>
        /// The timeslot start date
        /// </summary>
        /// <example>01/01/2018 12:15:30</example>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The average price
        /// </summary>
        /// <example>4.00</example>
        public double Price { get; private set; }

        public static AveragePriceDto Create(DateTime startDate, double averagePrice)
        {
            return new AveragePriceDto
            {
                Date = startDate,
                Price = Math.Round(averagePrice, 2)
            };
        }
    }
}
