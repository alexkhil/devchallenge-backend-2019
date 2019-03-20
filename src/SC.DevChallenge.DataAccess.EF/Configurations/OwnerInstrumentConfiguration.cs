using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class OwnerInstrumentConfiguration : IEntityTypeConfiguration<OwnerInstrument>
    {
        public void Configure(EntityTypeBuilder<OwnerInstrument> builder)
        {
            builder.ToTable(Tables.OwnerInstruments, Schemas.Dbo);

            builder.HasKey(oi => new { oi.OwnerId, oi.InstrumentId });

            builder.HasOne(oi => oi.Instrument)
                .WithMany(i => i.OwnerInstruments)
                .HasForeignKey(oi => oi.InstrumentId);

            builder.HasOne(oi => oi.Owner)
                .WithMany(o => o.OwnerInstruments)
                .HasForeignKey(oi => oi.OwnerId);
        }
    }
}
