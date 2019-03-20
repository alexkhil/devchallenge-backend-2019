using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable(Tables.Portfolios, Schemas.Dbo);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasMany(p => p.OwnerPortfolios)
                .WithOne(op => op.Portfolio)
                .HasForeignKey(op => op.PortfolioId);
        }
    }
}
