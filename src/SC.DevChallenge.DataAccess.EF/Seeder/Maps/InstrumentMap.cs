using CsvHelper.Configuration;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Maps
{
    public class InstrumentMap : ClassMap<Instrument>
    {
        public InstrumentMap()
        {
            Map(m => m.Name).Name("Instrument");
        }
    }
}
