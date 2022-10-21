using Microsoft.EntityFrameworkCore;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Querries;
using System.Linq.Expressions;

namespace Identity.API.Tests.Repository;

public class RequestMockRepository : IRequestRepository
{
    private List<RequestDbModel> _repositoryData;

    public RequestMockRepository()
    {
        _repositoryData = new List<RequestDbModel>();
    }

    public void InitialData(List<RequestDbModel> data)
    {
        _repositoryData = data;
    }

    public Task<RequestDbModel> AddAsync(RequestDbModel entity)
    {
        return Task.FromResult(entity);
    }

    public async Task DeleteAsync(RequestDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
    }

    public async Task<IReadOnlyList<RequestDbModel>> GetAllAsync()
    {
        return _repositoryData.ToList();
    }

    public async Task<IReadOnlyList<RequestDbModel>> GetAsync(Expression<Func<RequestDbModel, bool>> predicate)
    {
        return await _repositoryData.AsQueryable().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<RequestDbModel>> GetAsync(Expression<Func<RequestDbModel, bool>>? predicate = null,
                                                           Func<IQueryable<RequestDbModel>, IOrderedQueryable<RequestDbModel>>? orderBy = null,
                                                           string? includedString = null,
                                                           bool disableTracking = true)
    {
        IQueryable<RequestDbModel> query = _repositoryData.AsQueryable();
        if (disableTracking) query = query.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(includedString)) query = query.Include(includedString);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<RequestDbModel>> GetAsync(Expression<Func<RequestDbModel, bool>>? predicate = null,
                                                           Func<IQueryable<RequestDbModel>, IOrderedQueryable<RequestDbModel>>? orderBy = null,
                                                           List<Expression<Func<RequestDbModel, object>>>? includes = null,
                                                           bool disableTracking = true)
    {
        IQueryable<RequestDbModel> query = _repositoryData.AsQueryable();
        if (disableTracking) query = query.AsNoTracking();
        if (includes != null) includes.Aggregate(query, (current, include) => current.Include(include));
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<RequestDbModel?> GetByIdAsync(int id)
    {
        return _repositoryData.FirstOrDefault(e => e.Id == id);
    }

    public async Task<RequestDbModel?> GetFirstOrDefaultAsync(Expression<Func<RequestDbModel, bool>> predicate)
    {
        return _repositoryData.AsQueryable().FirstOrDefault(predicate);
    }

    public async Task UpdateAsync(RequestDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
    }

    public async Task<List<RequestDbModel>> GetRequestsByPage(GetAllRequestCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return _repositoryData.ToList();
        return _repositoryData.OrderByDescending(v => v.CreatedDate).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
    }

    public async Task<int> GetRequestsCount()
    {
        return _repositoryData.Count;
    }
}
