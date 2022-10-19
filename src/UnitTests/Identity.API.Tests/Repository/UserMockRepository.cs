using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.API.Tests.Repository;

public class UserMockRepository : IUserRepository
{
    private List<UserDbModel> _repositoryData;

    public UserMockRepository()
    {
        _repositoryData = new List<UserDbModel>();
    }

    public void InitialData(List<UserDbModel> data)
    {
        _repositoryData = data;
    }

    public async Task<UserDbModel> AddAsync(UserDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.Email == entity.Email
                                             || e.PhoneNumber == entity.PhoneNumber) == null) return entity;
        throw new InvalidOperationException("User with this data is exists");
    }

    public async Task DeleteAsync(UserDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.Email == entity.Email
                                             || e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
    }

    public async Task<IReadOnlyList<UserDbModel>> GetAllAsync()
    {
        return _repositoryData.ToList();
    }

    public async Task<IReadOnlyList<UserDbModel>> GetAsync(Expression<Func<UserDbModel, bool>> predicate)
    {
        return await _repositoryData.AsQueryable().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<UserDbModel>> GetAsync(Expression<Func<UserDbModel, bool>>? predicate = null,
                                                           Func<IQueryable<UserDbModel>, IOrderedQueryable<UserDbModel>>? orderBy = null,
                                                           string? includedString = null,
                                                           bool disableTracking = true)
    {
        IQueryable<UserDbModel> query = _repositoryData.AsQueryable();
        if (disableTracking) query = query.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(includedString)) query = query.Include(includedString);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<UserDbModel>> GetAsync(Expression<Func<UserDbModel, bool>>? predicate = null,
                                                           Func<IQueryable<UserDbModel>, IOrderedQueryable<UserDbModel>>? orderBy = null,
                                                           List<Expression<Func<UserDbModel, object>>>? includes = null,
                                                           bool disableTracking = true)
    {
        IQueryable<UserDbModel> query = _repositoryData.AsQueryable();
        if (disableTracking) query = query.AsNoTracking();
        if (includes != null) includes.Aggregate(query, (current, include) => current.Include(include));
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<UserDbModel?> GetByIdAsync(int id)
    {
        return _repositoryData.FirstOrDefault(e => e.Id == id);
    }

    public async Task<UserDbModel?> GetFirstOrDefaultAsync(Expression<Func<UserDbModel, bool>> predicate)
    {
        return _repositoryData.AsQueryable().FirstOrDefault(predicate);
    }

    public async Task<(IEnumerable<UserDbModel>, int)> GetPaginatedAll(string? filterString, int pageSize, int page)
    {
        IEnumerable<UserDbModel> data = _repositoryData;
        if (filterString != null)
        {
            data = data.Where(v => v.FirstName.ToLower().Contains(filterString.ToLower())
                                || v.MiddleName.ToLower().Contains(filterString.ToLower())
                                || v.LastName.ToLower().Contains(filterString.ToLower())
                                || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
                                || v.Email.ToLower().Contains(filterString.ToLower())
                                || v.Citizenship.ToLower().Contains(filterString.ToLower()));
        }
        return (data.OrderByDescending(v => v.RegistrationDate).Skip((page - 1) * pageSize).Take(pageSize), data.Count());
    }

    public async Task UpdateAsync(UserDbModel entity)
    {
        if (_repositoryData.FirstOrDefault(e => e.Email == entity.Email
                                             || e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
    }
}
