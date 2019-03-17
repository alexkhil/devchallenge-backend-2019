using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class Owner : EntityBase<int>
    {
        public string Name { get; set; }

        public ICollection<OwnerInstruments> OwnerInstruments { get; set; } = new HashSet<OwnerInstruments>();

        public ICollection<OwnerPortfolios> OwnerPortfolios { get; set; } = new HashSet<OwnerPortfolios>();
    }
}
