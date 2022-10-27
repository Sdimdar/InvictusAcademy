using Microsoft.EntityFrameworkCore;
using CommonRepository;
using Request.Domain.Entities;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;
using User.Domain.Entities;
using User.Infrastructure.Persistance;
using User.Infrastructure.Extensions;

namespace User.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext context) : base(context) { }

    public async Task<List<UserDbModel>> GetUsersByPage(GetAllUsersCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return await Context.Users.ToListAsync();
        IQueryable<UserDbModel> requestsPerPage = Context.Users.OrderByDescending(r => r.CreatedDate).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
        var result = await requestsPerPage.ToListAsync();
        return result;
    }

    public async Task<int> GetUsersCount()
    {
        IQueryable<UserDbModel> result = Context.Users;
        return result.Count();
    }
}
