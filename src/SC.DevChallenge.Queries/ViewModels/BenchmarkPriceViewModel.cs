using System;

namespace SC.DevChallenge.Queries.ViewModels
{
    public class BenchmarkPriceViewModel
    {
        /// <summary>
        /// The timeslot start date
        /// </summary>
        /// <example>01/01/2018 12:15:30</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Benchmark price
        /// </summary>
        /// <example>4.00</example>
        public double Price { get; set; }
    }
}
