using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.EF
{
    public partial class AppDbContext
    {
        public virtual DbSet<Instrument> Instruments { get; set; }

        public virtual DbSet<Owner> Owners { get; set; }

        public virtual DbSet<Portfolio> Portfolios { get; set; }

        public virtual DbSet<Price> Prices { get; set; }
    }
}