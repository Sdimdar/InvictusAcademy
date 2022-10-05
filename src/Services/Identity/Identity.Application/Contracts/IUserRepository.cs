﻿using System.Linq.Expressions;
using Identity.Application.Features.Users.Queries.GetUsersData;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(string id);
    Task<User> GetByPredicateAsync(Expression<Func<User, bool>> predicate);
    Task<UsersDataVm> GetPaginatedAll(string filterString, int pageSize, int page);
}
