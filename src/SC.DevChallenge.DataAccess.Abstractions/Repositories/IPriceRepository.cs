using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SC.DevChallenge.DataAccess.Abstractions.Entities;

namespace SC.DevChallenge.DataAccess.Abstractions.Repositories
{
    public interface IPriceRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<Price, bool>> wherePredicate,
            Expression<Func<Price, T>> projection);
    }
}
