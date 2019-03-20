using CsvHelper.Configuration;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Maps
{
    public class PortfolioMap : ClassMap<Portfolio>
    {
        public PortfolioMap()
        {
            Map(m => m.Name).Name("Portfolio");
        }
    }
}
