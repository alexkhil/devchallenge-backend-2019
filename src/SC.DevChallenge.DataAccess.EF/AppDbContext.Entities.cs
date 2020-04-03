using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF
{
    public sealed partial class AppDbContext
    {
        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Price> Prices { get; set; }
    }
}
