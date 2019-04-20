using System;
using System.ComponentModel.DataAnnotations;

namespace SC.DevChallenge.Dto.Prices.GetAggregatePrice
{
    public class GetAggregatePriceDto
    {
        [Required]
        public string Portfolio { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public int NumberOfAggregatedPrices { get; set; }
    }
}
