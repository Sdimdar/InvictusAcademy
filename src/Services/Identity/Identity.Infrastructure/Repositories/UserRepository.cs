using Identity.Application.Contracts;
using Identity.Infrastructure.Extensions;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using CommonRepository;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext context) : base(context) { }

    public async Task<(IEnumerable<UserDbModel>, int)> GetPaginatedAll(string? filterString, int pageSize, int page)
    {
        var result = await _context.Users.Filter(filterString)
                                         .GetABatchOfData(page, pageSize);
        return (result.Item1.ToArray(), result.Item2);
    }
}
