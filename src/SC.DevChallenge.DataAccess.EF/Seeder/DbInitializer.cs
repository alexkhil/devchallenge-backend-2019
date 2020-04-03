using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;
using SC.DevChallenge.Domain.Abstractions;
using SC.DevChallenge.Domain.Constants;

namespace SC.DevChallenge.DataAccess.EF.Seeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> logger;
        private readonly AppDbContext appDbContext;
        private readonly IDateTimeConverter dateTimeConverter;
        private readonly string filePath;

        public DbInitializer(
            ILogger<DbInitializer> logger,
            AppDbContext dbContext,
            IDateTimeConverter dateTimeConverter)
        {
            this.logger = logger;
            this.appDbContext = dbContext;
            this.dateTimeConverter = dateTimeConverter;
            this.filePath = Path.Combine(AppContext.BaseDirectory, $"Input{Path.DirectorySeparatorChar}data.csv");

            if (!File.Exists(this.filePath))
            {
                throw new InvalidOperationException("Input data file doesn't exists");
            }
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            await this.appDbContext.Database.EnsureDeletedAsync(cancellationToken);
            this.logger.LogInformation("Creating Db...");
            await this.appDbContext.Database.MigrateAsync(cancellationToken);

            this.logger.LogInformation("Seeding from {file}", this.filePath);

            await this.InitializeInternalAsync(this.filePath, cancellationToken);
        }

        private async Task InitializeInternalAsync(string csvFilePath, CancellationToken cancellationToken)
        {
            var rows = new List<DataRow>();
            using (var reader = new StreamReader(csvFilePath))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                rows.AddRange(csv.GetRecords<DataRow>().ToList());
            }

            var portfolios = rows.Select(r => r.Portfolio).Distinct().Select(p => new Portfolio { Name = p }).ToList();
            await this.appDbContext.BulkInsertAsync(portfolios, cancellationToken: cancellationToken);

            var owners = rows.Select(r => r.Owner).Distinct().Select(p => new Owner { Name = p }).ToList();
            await this.appDbContext.BulkInsertAsync(owners, cancellationToken: cancellationToken);

            var instruments = rows.Select(r => r.Instrument).Distinct().Select(p => new Instrument { Name = p }).ToList();
            await this.appDbContext.BulkInsertAsync(instruments, cancellationToken: cancellationToken);

            await this.appDbContext.SaveChangesAsync(cancellationToken);

            var portfoliosMap = await this.appDbContext.Portfolios
                .AsNoTracking()
                .ToDictionaryAsync(p => p.Name, v => v.Id, cancellationToken);

            var ownersMap = await this.appDbContext.Owners
                .AsNoTracking()
                .ToDictionaryAsync(p => p.Name, v => v.Id, cancellationToken);

            var instrumentsMap = await this.appDbContext.Instruments
                .AsNoTracking()
                .ToDictionaryAsync(p => p.Name, v => v.Id, cancellationToken);

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
                    Timeslot = this.dateTimeConverter.DateTimeToTimeSlot(date),
                    Value = r.Price
                });
            }

            await this.appDbContext.BulkInsertAsync(instrumentPrices.ToList(), cancellationToken: cancellationToken);
            await this.appDbContext.BulkInsertAsync(ownerPortfolios.ToList(), cancellationToken: cancellationToken);
            await this.appDbContext.BulkInsertAsync(ownerInstruments.ToList(), cancellationToken: cancellationToken);
            await this.appDbContext.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Seed finished");
        }
    }
}
