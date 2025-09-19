// Repositories/Interfaces/IGenericRepository.cs
using UniversitySystem.Specifications;
using System.Linq.Expressions;

namespace UniversitySystem.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);

        Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> spec, CancellationToken ct = default);

        Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default);

        Task<T> AddAsync(T entity, CancellationToken ct = default);
        
        Task<T?> GetSingleAsync(ISpecification<T> spec, CancellationToken ct = default);

        Task<T> UpdateAsync(T entity, CancellationToken ct = default);

        Task DeleteAsync(T entity, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
    }
}