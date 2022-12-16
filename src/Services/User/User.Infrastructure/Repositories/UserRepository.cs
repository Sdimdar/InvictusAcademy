using CommonRepository;
using Microsoft.EntityFrameworkCore;
using User.Application.Contracts;
using User.Domain.Entities;
using User.Infrastructure.Persistance;

namespace User.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext context) : base(context) { }

    protected override IQueryable<UserDbModel> FilterByString(IQueryable<UserDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.FirstName.ToLower().Contains(filterString.ToLower())
                                     || v.MiddleName!.ToLower().Contains(filterString.ToLower())
                                     || v.LastName.ToLower().Contains(filterString.ToLower())
                                     || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
                                     || v.Email.ToLower().Contains(filterString.ToLower())
                                     || v.Citizenship!.ToLower().Contains(filterString.ToLower())
            );
    }

    public async Task<List<UserDbModel>> GetUsersByIdList(List<int> usersId)
    {
        List<UserDbModel> list = new();
        foreach (var item in usersId)
        {
            var query = await Context.Users.FirstOrDefaultAsync(c => c.Id == item);
            if (query is not null)
                list.Add(query);
        }

        return list;
    }
}
