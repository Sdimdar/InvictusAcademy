using CommonRepository.Abstractions;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts;

public interface IUserRepository : IBaseRepository<User>
{
    Task<(IEnumerable<User>, int)> GetPaginatedAll(string? filterString, int pageSize, int page);
}
