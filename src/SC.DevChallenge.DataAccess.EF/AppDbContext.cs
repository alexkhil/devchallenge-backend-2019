using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.Configuration.Abstractions;

namespace SC.DevChallenge.DataAccess.EF
{
    public partial class AppDbContext : DbContext
    {
        private readonly IDbConfiguration configuration;

        public AppDbContext(IDbConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

            optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}