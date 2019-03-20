using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class Portfolio : EntityBase<int>
    {
        public string Name { get; set; }

        public ICollection<OwnerPortfolio> OwnerPortfolios { get; set; } = new HashSet<OwnerPortfolio>();
    }
}
