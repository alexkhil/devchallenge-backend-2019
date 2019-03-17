using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Abstractions
{
    public interface IEntityTypeSeed<TEntity> where TEntity: class
    {
        void Seed(EntityTypeBuilder<TEntity> builder);
    }
}
