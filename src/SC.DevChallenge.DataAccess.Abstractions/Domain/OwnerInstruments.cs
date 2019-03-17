using System;
using System.Collections.Generic;
using System.Text;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class OwnerInstruments
    {
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }
    }
}
