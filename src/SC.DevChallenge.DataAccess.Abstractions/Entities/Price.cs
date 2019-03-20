using System;
using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class Price
    {
        public int PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }

        public DateTime Date { get; set; }

        public float Value { get; set; }
    }
}
