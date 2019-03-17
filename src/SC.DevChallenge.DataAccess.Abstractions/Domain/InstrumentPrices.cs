using System;
using System.Collections.Generic;
using System.Text;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class InstrumentPrices
    {
        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }

        public int PriceId { get; set; }

        public Price Price { get; set; }
    }
}
