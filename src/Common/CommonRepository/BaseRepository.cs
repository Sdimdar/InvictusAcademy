using CommonRepository.Abstractions;
using CommonRepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommonRepository;

public class BaseRepository<T, TC> : IBaseRepository<T> where T: BaseRepositoryEntity where TC : DbContext
{
    protected readonly TC Context;
    public BaseRepository(TC dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }
    public virtual async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                         string? includedString = null,
                                                         bool disableTracking = true)
    {
        IQueryable<T> query = Context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(includedString)) query = query.Include(includedString);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                         List<Expression<Func<T, object>>>? includes = null,
                                                         bool disableTracking = true)
    {
        IQueryable<T> query = Context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (includes != null) includes.Aggregate(query, (current, include) => current.Include(include));
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>()
                              .FirstOrDefaultAsync(i => i.Id == id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        entity.CreatedDate = DateTime.Now;
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        entity.LastModifiedDate = DateTime.Now;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>()
                              .FirstOrDefaultAsync(predicate);
    }
}