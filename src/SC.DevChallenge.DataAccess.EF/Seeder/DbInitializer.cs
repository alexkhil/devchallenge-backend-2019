using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using SC.DevChallenge.DataAccess.Abstractions.Domain;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;

namespace SC.DevChallenge.DataAccess.EF.Seeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext dbContext;

        public DbInitializer(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task InitializeAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Input data file doesn't exists", nameof(filePath));
            }

            return InitializeInternalAsync(filePath);
        }

        public async Task InitializeInternalAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<DataRow>();
                    await CreatePortfoliosAsync(records.Select(r => r.Portfolio).Distinct());
                    await CreateOwnersAsync(records.Select(o => o.Owner).Distinct());
                    await CreateInstrumentsAsync(records.Select(i => i.Instrument).Distinct());
                }
            }
        }

        private Task CreatePortfoliosAsync(IEnumerable<string> portfolios)
        {
            var data = portfolios.Select(p => new Portfolio { Name = p });
            return dbContext.AddRangeAsync(data);
        }

        private Task CreateOwnersAsync(IEnumerable<string> owners)
        {
            var data = owners.Select(o => new Owner { Name = o });
            return dbContext.AddRangeAsync(data);
        }

        private Task CreateInstrumentsAsync(IEnumerable<string> instruments)
        {
            var data = instruments.Select(i => new Instrument { Name = i });
            return dbContext.AddRangeAsync(data);
        }
    }
}