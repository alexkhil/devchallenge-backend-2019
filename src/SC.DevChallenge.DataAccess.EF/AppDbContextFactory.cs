using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SC.DevChallenge.Configuration.DataAccess;

namespace SC.DevChallenge.DataAccess.EF
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DevChallenge;Trusted_Connection=True;MultipleActiveResultSets=true";

        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new DbConfiguration
            {
                ConnectionString = connectionString
            };

            return new AppDbContext(config);
        }
    }
}
