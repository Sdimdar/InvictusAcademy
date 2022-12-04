using User.Domain.Entities;

namespace User.Infrastructure.Extensions;

public static class UserRepositoryExtensions
{
    public static IQueryable<UserDbModel> Filter(this IQueryable<UserDbModel> users, string? filterByName)
    {
        return string.IsNullOrEmpty(filterByName)
            ? users
            : users.Where(v => v.FirstName.ToLower().Contains(filterByName.ToLower())
                               || v.MiddleName.ToLower().Contains(filterByName.ToLower())
                               || v.LastName.ToLower().Contains(filterByName.ToLower())
                               || v.PhoneNumber.ToLower().Contains(filterByName.ToLower())
                               || v.Email.ToLower().Contains(filterByName.ToLower())
                               || v.Citizenship.ToLower().Contains(filterByName.ToLower())
            );
    }

    public static IQueryable<UserDbModel> GetABatchOfData(this IQueryable<UserDbModel> users, int page, int pageSize)
    {
        return users.OrderByDescending(v => v.RegistrationDate).Skip((page - 1) * pageSize).Take(pageSize);
    }
}
