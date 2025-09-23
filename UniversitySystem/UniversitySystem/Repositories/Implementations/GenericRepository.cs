// Repositories/Implementations/GenericRepository.cs
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;

namespace UniversitySystem.Repositories.Implementations
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _dbSet.FindAsync(new object[] { id }, ct);

        public async Task<T?> GetByCodeAsync(string code, CancellationToken ct = default)
            => await _dbSet.FindAsync(new object[] { code }, ct);

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
            => await _dbSet.ToListAsync(ct);

        public async Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = _dbSet.AsQueryable();
            query = ApplySpecification(query, spec);
            return await query.ToListAsync(ct);
        }

        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = _dbSet.AsQueryable();
            query = ApplySpecification(query, spec);
            return await query.CountAsync(ct);
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await context.SaveChangesAsync(ct);
            return entity;
        }
        
        public async Task<T?> GetSingleAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = _dbSet.AsQueryable();
            query = ApplySpecification(query, spec);
            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync(ct);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            await _dbSet.AddRangeAsync(entities, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            _dbSet.UpdateRange(entities);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            _dbSet.RemoveRange(entities);
            await context.SaveChangesAsync(ct);
        }

        private IQueryable<T> ApplySpecification(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;


            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }


            if (spec.Includes?.Any() == true)
            {
                query = spec.Includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }


            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if (spec.IsPagingEnabled)
            {
                if (spec.Skip.HasValue)
                {
                    query = query.Skip(spec.Skip.Value);
                }
                if (spec.Take.HasValue)
                {
                    query = query.Take(spec.Take.Value);
                }
            }

            return query;
        }
    }
}