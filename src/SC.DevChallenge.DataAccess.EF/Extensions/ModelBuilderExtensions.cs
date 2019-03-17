using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataAccess.EF.Seeder.Abstractions;

namespace SC.DevChallenge.DataAccess.EF.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplySeed<TEntity>(
            this ModelBuilder builder,
            IEntityTypeSeed<TEntity> seeder) where TEntity: class
        {
            if (seeder != null)
            {
                seeder.Seed(builder.Entity<TEntity>());
            }

            return builder;
        }
    }
}
