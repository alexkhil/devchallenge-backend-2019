using System;
using System.Linq.Expressions;

namespace SC.DevChallenge.Queries.Abstractions
{
    public abstract class SpecificationBase<TEntity, TParam> : ISpecification<TEntity, TParam>
    {
        public bool IsSatisfiedBy(TEntity entity, TParam request)
        {
            var predicate = this.ToExpression(request).Compile(preferInterpretation: true);
            return predicate.Invoke(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression(TParam request);
    }
}
