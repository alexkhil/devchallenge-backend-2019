using CsvHelper.Configuration;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Maps
{
    public class OwnerMap : ClassMap<Owner>
    {
        public OwnerMap()
        {
            Map(o => o.Name).Name("Owner");
        }
    }
}
