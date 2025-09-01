using CompanyApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace CompanyApi.Repositories;


public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = context;


    public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();
        if (filter != null)
            query = query.Where(filter);


        foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);


        var total = await query.CountAsync();
        if (orderBy != null)
            query = orderBy(query);


        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }


    public async Task<T?> GetByIdAsync(object id) => await _context.Set<T>().FindAsync(id);


    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }


    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(object id)
    {
        var entity = await GetByIdAsync(id) ?? throw new KeyNotFoundException();
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}