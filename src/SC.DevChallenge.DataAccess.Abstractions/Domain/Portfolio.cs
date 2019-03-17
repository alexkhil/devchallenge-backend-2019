using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class Portfolio : EntityBase<int>
    {
        public string Name { get; set; }

        public ICollection<OwnerPortfolios> OwnerPortfolios { get; set; } = new HashSet<OwnerPortfolios>();
    }
}
