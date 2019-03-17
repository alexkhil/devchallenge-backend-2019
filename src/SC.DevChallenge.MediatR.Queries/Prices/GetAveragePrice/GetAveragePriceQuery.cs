using System;
using MediatR;
using SC.DevChallenge.Dto;

namespace SC.DevChallenge.MediatR.Queries.Prices.GetAverage
{
    public class GetAveragePriceQuery : IRequest<AveragePriceDto>
    {
        public string Portfolio { get; set; }

        public string Owner { get; set; }

        public string Instrument { get; set; }

        public DateTime Date { get; set; }
    }
}
