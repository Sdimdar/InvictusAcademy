using CommonRepository.Abstractions;
using ServicesContracts.Identity.Responses;
using User.Domain.Entities;

namespace User.Application.Contracts;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    Task<List<UserDbModel>> GetUsersByPage(GetAllUsersCommand pageInfo);
    Task<int> GetUsersCount();
}
