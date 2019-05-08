using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;
using SC.DevChallenge.Domain.Constants;
using SC.DevChallenge.Domain.DateTimeConverter;

namespace SC.DevChallenge.DataAccess.EF.Seeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> logger;
        private readonly AppDbContext dbContext;
        private readonly IDateTimeConverter dateTimeConverter;
        private readonly string filePath;

        public DbInitializer(
            ILogger<DbInitializer> logger,
            AppDbContext dbContext,
            IDateTimeConverter dateTimeConverter)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.dateTimeConverter = dateTimeConverter;
            filePath = Path.Combine(AppContext.BaseDirectory, $"Input{Path.DirectorySeparatorChar}data.csv");

            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("Input data file doesn't exists");
            }
        }

        public Task InitializeAsync()
        {
            dbContext.Database.EnsureDeleted();
            logger.LogInformation("Creating Db...");
            dbContext.Database.Migrate();

            logger.LogInformation("Seeding from {file}", filePath);

            return InitializeInternalAsync(filePath);
        }

        public async Task InitializeInternalAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader))
                {
                    var rows = csv.GetRecords<DataRow>().ToList();

                    var portfolios = rows.Select(r => r.Portfolio).Distinct().Select(p => new Portfolio { Name = p }).ToList();
                    await dbContext.BulkInsertAsync(portfolios);
                    logger.LogInformation("{Count} portfolios created", portfolios.Count);

                    var owners = rows.Select(r => r.Owner).Distinct().Select(p => new Owner { Name = p }).ToList();
                    await dbContext.BulkInsertAsync(owners);
                    logger.LogInformation("{Count} owners created", owners.Count);

                    var instruments = rows.Select(r => r.Instrument).Distinct().Select(p => new Instrument { Name = p }).ToList();
                    await dbContext.BulkInsertAsync(instruments);
                    logger.LogInformation("{Count} instruments created", instruments.Count);

                    var portfoliosMap = await dbContext.Portfolios.AsNoTracking().ToDictionaryAsync(p => p.Name, v => v.Id);
                    var ownersMap = await dbContext.Owners.AsNoTracking().ToDictionaryAsync(p => p.Name, v => v.Id);
                    var instrumentsMap = await dbContext.Instruments.AsNoTracking().ToDictionaryAsync(p => p.Name, v => v.Id);

                    dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                    
                    var ownerPortfolios = new HashSet<OwnerPortfolio>(new OwnerPortfoliosComparer());
                    var ownerInstruments = new HashSet<OwnerInstrument>(new OwnerInstrumentComparer());
                    var instrumentPrices = new HashSet<Price>();

                    foreach (var r in rows)
                    {
                        ownerPortfolios.Add(new OwnerPortfolio
                        {
                            OwnerId = ownersMap[r.Owner],
                            PortfolioId = portfoliosMap[r.Portfolio]
                        });

                        ownerInstruments.Add(new OwnerInstrument
                        {
                            OwnerId = ownersMap[r.Owner],
                            InstrumentId = instrumentsMap[r.Instrument]
                        });

                        var date = DateTime.ParseExact(r.Date, DateFormat.Default, CultureInfo.InvariantCulture);

                        instrumentPrices.Add(new Price
                        {
                            PortfolioId = portfoliosMap[r.Portfolio],
                            OwnerId = ownersMap[r.Owner],
                            InstrumentId = instrumentsMap[r.Instrument],
                            Date = date,
                            Timeslot = dateTimeConverter.DateTimeToTimeSlot(date),
                            Value = r.Price
                        });
                    }

                    await dbContext.BulkInsertAsync(instrumentPrices);
                    logger.LogInformation("{Count} prices created", instrumentPrices.Count);

                    await dbContext.BulkInsertAsync(ownerPortfolios);
                    await dbContext.BulkInsertAsync(ownerInstruments);
                }
            }
        }
    }
}