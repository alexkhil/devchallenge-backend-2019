using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class Owner : EntityBase<int>
    {
        public string Name { get; set; }

        public ICollection<OwnerInstrument> OwnerInstruments { get; set; } = new HashSet<OwnerInstrument>();

        public ICollection<OwnerPortfolio> OwnerPortfolios { get; set; } = new HashSet<OwnerPortfolio>();
    }
}
