using System.ComponentModel.DataAnnotations;

namespace SC.DevChallenge.Dto
{
    /// <summary>
    /// Response of GetAveragePrice
    /// </summary>
    public class GetAveragePriceDto
    {
        /// <summary>
        /// The name of Portfolio
        /// </summary>
        /// <example>Fannie Mae</example>
        [Required]
        public string Portfolio { get; set; }

        /// <summary>
        /// The name of Owner
        /// </summary>
        /// <example>SimCorp</example>
        [Required]
        public string Owner { get; set; }

        /// <summary>
        /// The name of Instrument
        /// </summary>
        /// <example>Equity</example>
        [Required]
        public string Instrument { get; set; }

        /// <summary>
        /// The date of timeslot
        /// </summary>
        /// <example>15/03/2018 17:26:40</example>
        [Required]
        public string Date { get; set; }
    }
}
