using System;
using System.Linq.Expressions;

namespace UniversitySystem.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}