using CommonRepository.Abstractions;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    Task<(IEnumerable<UserDbModel>, int)> GetPaginatedAll(string? filterString, int pageSize, int page);
}
