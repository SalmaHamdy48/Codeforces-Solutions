
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Specifications;
using System.Linq.Expressions;

namespace UniversitySystem.Specifications
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> ApplySpecification(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Includes != null && specification.Includes.Any())
            {
                query = specification.Includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                {
                    query = query.Skip(specification.Skip.Value);
                }
                if (specification.Take.HasValue)
                {
                    query = query.Take(specification.Take.Value);
                }
            }

            return query;
        }
        
    }
}