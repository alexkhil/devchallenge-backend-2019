using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Maps
{
    public class PriceMap : ClassMap<Price>
    {
        public PriceMap()
        {
            Map(m => m.Date).Name("Date").TypeConverter<DateTimeConverter>();
            Map(m => m.Value).Name("Price");
        }
    }
}
