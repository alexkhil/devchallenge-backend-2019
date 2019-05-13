using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.Abstractions.Repositories
{
    public interface IPriceRepository
    {
        Task<List<double>> GetAveragePricesAsync();

        Task<Dictionary<int, double>> GetAveragePricesAsync(int startTimeslot, int endTimeslot);

        Task<int> GetPricesCount(int timeslot);

        Task<List<Price>> GetAllAsync(
            Expression<Func<Price, bool>> wherePredicate);

        Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<Price, bool>> wherePredicate,
            Expression<Func<Price, T>> projection);
    }
}
