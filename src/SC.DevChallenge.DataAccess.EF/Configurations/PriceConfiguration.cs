using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable(Tables.Prices, Schemas.Dbo);

            builder.HasKey(p => new { p.PortfolioId, p.OwnerId, p.InstrumentId, p.Date });

            builder.HasOne(p => p.Portfolio)
                .WithMany()
                .HasForeignKey(p => p.PortfolioId);

            builder.HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId);

            builder.HasOne(p => p.Instrument)
                .WithMany()
                .HasForeignKey(p => p.InstrumentId);

            builder.Property(p => p.Value).IsRequired();
        }
    }
}
