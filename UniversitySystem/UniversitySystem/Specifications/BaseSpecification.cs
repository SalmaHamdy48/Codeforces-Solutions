using System;
using System.Linq.Expressions;

namespace UniversitySystem.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
    }
}