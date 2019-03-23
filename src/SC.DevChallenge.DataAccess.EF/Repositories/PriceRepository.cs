using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataAccess.Abstractions.Entities;
using SC.DevChallenge.DataAccess.Abstractions.Repositories;

namespace SC.DevChallenge.DataAccess.EF.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AppDbContext dbContext;

        public PriceRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<Price, bool>> wherePredicate,
            Expression<Func<Price, T>> projection)
        {
            return await dbContext.Prices
                .Where(wherePredicate)
                .Select(projection)
                .ToListAsync();
        }
    }
}
