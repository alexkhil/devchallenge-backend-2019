using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class Instrument : EntityBase<int>
    {
        public string Name { get; set; }

        public ICollection<OwnerInstruments> OwnerInstruments { get; set; } = new HashSet<OwnerInstruments>();

        public ICollection<InstrumentPrices> InstrumentPrices { get; set; } = new HashSet<InstrumentPrices>();
    }
}
