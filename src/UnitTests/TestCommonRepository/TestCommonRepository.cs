using System.Linq.Expressions;
using CommonRepository.Abstractions;
using CommonRepository.Models;

namespace TestCommonRepository;

public abstract class TestCommonRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseRepositoryEntity
{
    protected List<TEntity> Context { get; set; }

    protected TestCommonRepository()
    {
        Context = new List<TEntity>();
    }
    
    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return Task.FromResult(Context.ToList());
    }

    public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, 
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy)
    {
        var query = Context.AsQueryable().Where(predicate);
        return Task.FromResult(orderBy != null ? orderBy(query).ToList() : query.ToList());
    }

    public virtual Task<TEntity?> GetByIdAsync(int id)
    {
        return Task.FromResult(Context.FirstOrDefault(e => e.Id == id));
    }

    public virtual Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Task.FromResult(Context.AsQueryable().FirstOrDefault(predicate));
    }

    public virtual Task<TEntity> AddAsync(TEntity entity)
    {
        return Task.FromResult(entity);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        if (Context.FirstOrDefault(e => e.Id == entity.Id) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        if (Context.FirstOrDefault(e => e.Id == entity.Id) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    public virtual Task<int> GetCountAsync()
    {
        return Task.FromResult(Context.Count);
    }

    public virtual Task<List<TEntity>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null)
    {
        var query = FilterByString(Context.AsQueryable(), filterString).OrderByDescending(e => e.LastModifiedDate)
                                                                       .Skip((page - 1) * pageSize)
                                                                       .Take(pageSize);
        return Task.FromResult(query.ToList());
    }
    
    protected abstract IQueryable<TEntity> FilterByString(IQueryable<TEntity> query, string? filterString);
}