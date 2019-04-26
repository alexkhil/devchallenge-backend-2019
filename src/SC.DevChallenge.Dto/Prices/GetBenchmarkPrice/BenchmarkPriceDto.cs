using System;

namespace SC.DevChallenge.Dto
{
    public class BenchmarkPriceDto
    {
        protected BenchmarkPriceDto()
        {
        }

        /// <summary>
        /// The timeslot start date
        /// </summary>
        /// <example>01/01/2018 12:15:30</example>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Benchmark price
        /// </summary>
        /// <example>4.00</example>
        public double Price { get; private set; }

        public static BenchmarkPriceDto Create(DateTime startDate, double averagePrice)
        {
            return new BenchmarkPriceDto
            {
                Date = startDate,
                Price = Math.Round(averagePrice, 2)
            };
        }
    }
}
