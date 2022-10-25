using CommonRepository;
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
}
