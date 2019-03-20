using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.EF.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    public class OwnerPortfoliosConfiguration : IEntityTypeConfiguration<OwnerPortfolio>
    {
        public void Configure(EntityTypeBuilder<OwnerPortfolio> builder)
        {
            builder.ToTable(Tables.OwnerPortfolios, Schemas.Dbo);

            builder.HasKey(op => new { op.OwnerId, op.PortfolioId });

            builder.HasOne(op => op.Owner)
                .WithMany(o => o.OwnerPortfolios)
                .HasForeignKey(op => op.OwnerId);

            builder.HasOne(op => op.Portfolio)
                .WithMany(p => p.OwnerPortfolios)
                .HasForeignKey(op => op.PortfolioId);
        }
    }
}
