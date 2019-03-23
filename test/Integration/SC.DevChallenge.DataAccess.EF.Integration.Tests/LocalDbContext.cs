using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.Configuration.Abstractions;

namespace SC.DevChallenge.DataAccess.EF.Integration.Tests
{
    public class LocalDbContext : AppDbContext
    {
        public LocalDbContext() : this(null)
        {
        }

        protected LocalDbContext(IDbConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }

            optionsBuilder.UseInMemoryDatabase("InMemoryAppDb");
        }
    }
}
