using CommonRepository.Abstractions;
using User.Domain.Entities;

namespace User.Application.Contracts;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    Task<(IEnumerable<UserDbModel>, int)> GetPaginatedAll(string? filterString, int pageSize, int page);
}
