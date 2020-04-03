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

        public async Task<List<T>> GetAllAsync<T>(
            Expression<Func<Price, bool>> wherePredicate,
            Expression<Func<Price, T>> projection) =>
            await this.dbContext.Prices
                .Where(wherePredicate)
                .Select(projection)
                .ToListAsync();

        public async Task<List<Price>> GetAllAsync(Expression<Func<Price, bool>> wherePredicate) =>
            await this.dbContext.Prices
                .Where(wherePredicate)
                .ToListAsync();

        public async Task<List<double>> GetAveragePricesAsync() =>
            await this.dbContext.Prices
                .GroupBy(p => p.Timeslot)
                .Select(g => g.Average(p => p.Value))
                .OrderBy(x => x)
                .ToListAsync();

        public async Task<int> GetPricesCount(int timeslot) =>
            await this.dbContext.Prices.CountAsync(p => p.Timeslot == timeslot);

        public async Task<Dictionary<int, double>> GetAveragePricesAsync(
            string portfolio,
            int startTimeslot,
            int endTimeslot)
        {
            var timeslots = await this.dbContext.Prices
                .Where(x => x.Portfolio.Name == portfolio && x.Timeslot >= startTimeslot && x.Timeslot <= endTimeslot)
                .Select(x => new {x.Timeslot, x.Value})
                .ToListAsync();

            return timeslots.GroupBy(x => x.Timeslot, x => x.Value).ToDictionary(x => x.Key, x => x.Average());
        }
    }
}
