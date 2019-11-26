using System;
using System.ComponentModel.DataAnnotations;

namespace SC.DevChallenge.Api.Requests.Prices
{
    public class GetAggregatePriceDto
    {
        [Required]
        public string Portfolio { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ResultPoints { get; set; }
    }
}
