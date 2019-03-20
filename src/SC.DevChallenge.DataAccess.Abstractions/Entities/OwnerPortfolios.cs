using System;
using System.Collections.Generic;

namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class OwnerPortfolio
    {
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }

    public class OwnerPortfoliosComparer : IEqualityComparer<OwnerPortfolio>
    {
        public bool Equals(OwnerPortfolio x, OwnerPortfolio y)
        {
            return x.OwnerId == y.OwnerId && x.PortfolioId == y.PortfolioId;
        }

        public int GetHashCode(OwnerPortfolio obj)
        {
            return obj.OwnerId.GetHashCode() ^ obj.PortfolioId.GetHashCode();
        }
    }
}