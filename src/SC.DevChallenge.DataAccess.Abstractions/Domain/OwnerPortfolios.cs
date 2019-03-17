namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class OwnerPortfolios
    {
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}