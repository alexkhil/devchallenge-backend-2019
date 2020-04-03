using System;
using System.Linq.Expressions;

namespace SC.DevChallenge.Queries.Abstractions
{
    public interface ISpecification<TEntity, TParam>
    {
        bool IsSatisfiedBy(TEntity entity, TParam request);

        Expression<Func<TEntity, bool>> ToExpression(TParam request);
    }
}
