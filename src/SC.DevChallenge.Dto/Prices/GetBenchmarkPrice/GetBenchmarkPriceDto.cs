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

        [Required]
        public string Portfolio { get; set; }
    }
}
