using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable(Tables.Owners, Schemas.Dbo);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired();
            builder.HasIndex(o => o.Name).IsUnique();

            builder.HasMany(o => o.OwnerInstruments)
                .WithOne(oi => oi.Owner)
                .HasForeignKey(oi => oi.OwnerId);

            builder.HasMany(o => o.OwnerPortfolios)
                .WithOne(op => op.Owner)
                .HasForeignKey(o => o.OwnerId);
        }
    }
}
