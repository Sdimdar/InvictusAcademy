using Microsoft.EntityFrameworkCore;
using CommonRepository;
using User.Application.Contracts;
using User.Domain.Entities;
using User.Infrastructure.Persistance;
using User.Infrastructure.Extensions;

namespace User.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext context) : base(context) { }

    public async Task<IEnumerable<UserDbModel>> GetPaginatedAll(string? filterString, int pageSize, int page)
    {
        var result = Context.Users.Filter(filterString)
                                  .GetABatchOfData(page, pageSize);
        return await result.ToListAsync();
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await Context.Users.CountAsync();
    }
}
