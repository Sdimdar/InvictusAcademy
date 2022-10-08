using Identity.Application.Contracts;
using Identity.Infrastructure.Extensions;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using CommonRepository;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext context) : base(context) { }

    public async Task<(IEnumerable<User>, int)> GetPaginatedAll(string? filterString, int pageSize, int page)
    {
        var result = await _context.Users.Filter(filterString)
                                         .GetABatchOfData(page, pageSize);
        return (result.Item1.ToArray(), result.Item2);
    }
}
