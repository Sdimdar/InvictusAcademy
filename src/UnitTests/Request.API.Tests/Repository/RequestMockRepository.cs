using Microsoft.EntityFrameworkCore;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Querries;
using System.Linq.Expressions;

namespace Request.API.Tests.Repository;

public class RequestMockRepository : IRequestRepository
{
    private readonly List<RequestDbModel> _repositoryData;

    public RequestMockRepository()
    {
        _repositoryData = new List<RequestDbModel>()
        {
            new RequestDbModel()
            {
                Id = 1,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 2,
                PhoneNumber = "89348473402",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 3,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 4,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            }
        };
    }

    public Task<RequestDbModel> AddAsync(RequestDbModel entity)
    {
        return Task.FromResult(entity);
    }

    public Task DeleteAsync(RequestDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<RequestDbModel>> GetAllAsync()
    {
        return Task.FromResult((IReadOnlyList<RequestDbModel>)_repositoryData.ToList());
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

    public Task<RequestDbModel?> GetByIdAsync(int id)
    {
        return Task.FromResult(_repositoryData.FirstOrDefault(e => e.Id == id));
    }

    public Task<RequestDbModel?> GetFirstOrDefaultAsync(Expression<Func<RequestDbModel, bool>> predicate)
    {
        return Task.FromResult(_repositoryData.AsQueryable().FirstOrDefault(predicate));
    }

    public Task UpdateAsync(RequestDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    public Task<List<RequestDbModel>> GetRequestsByPage(GetAllRequestCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return Task.FromResult(_repositoryData.ToList());
        return Task.FromResult(_repositoryData.OrderByDescending(v => v.CreatedDate).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList());
    }

    public Task<int> GetRequestsCount()
    {
        return Task.FromResult(_repositoryData.Count);
    }
}
