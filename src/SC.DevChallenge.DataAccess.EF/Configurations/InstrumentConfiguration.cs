using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
        {
            builder.ToTable(Tables.Instruments, Schemas.Dbo);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).IsRequired();
            builder.HasIndex(i => i.Name).IsUnique();

            builder.HasMany(i => i.OwnerInstruments)
                .WithOne(oi => oi.Instrument)
                .HasForeignKey(oi => oi.InstrumentId);
        }
    }
}
