using System.ComponentModel.DataAnnotations;

namespace SC.DevChallenge.Dto.Prices.GetBenchmarkPrice
{
    public class GetBenchmarkPriceDto
    {
        /// <summary>
        /// The timeslot start date
        /// </summary>
        /// <example>01/01/2018 12:15:30</example>
        [Required]
        public string Date { get; set; }

        /// <summary>
        /// Benchmark price
        /// </summary>
        /// <example>4.00</example>
        [Required]
        public double Price { get; set; }
    }
}
