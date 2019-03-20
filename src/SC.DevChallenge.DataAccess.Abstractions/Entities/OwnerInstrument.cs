using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class OwnerInstrument
    {
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }
    }

    public class OwnerInstrumentComparer : IEqualityComparer<OwnerInstrument>
    {
        public bool Equals(OwnerInstrument x, OwnerInstrument y)
        {
            return x.InstrumentId == y.InstrumentId && x.OwnerId == y.OwnerId;
        }

        public int GetHashCode(OwnerInstrument obj)
        {
            return obj.InstrumentId.GetHashCode() ^ obj.OwnerId.GetHashCode();
        }
    }
}
