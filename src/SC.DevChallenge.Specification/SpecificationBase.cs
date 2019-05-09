using System;
using System.Linq.Expressions;
using SC.DevChallenge.Specification.Abstractions;

namespace SC.DevChallenge.Specification
{
    public abstract class SpecificationBase<TEntity, TParam> : ISpecification<TEntity, TParam>
    {
        public bool IsSatisfiedBy(TEntity entity, TParam request)
        {
            var predicate = ToExpression(request).Compile(preferInterpretation: true);
            return predicate.Invoke(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression(TParam request);
    }
}
