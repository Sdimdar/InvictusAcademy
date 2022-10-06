using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Extensions;

public static class UserRepositoryExtensions
{
    public static IQueryable<User> Filter(this IQueryable<User> users, string? filterByName)
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

    public static async Task<(IQueryable<User>, int)> GetABatchOfData(this IQueryable<User> users, int page, int pageSize)
    {
        int count = await users.CountAsync();
        return (users.OrderByDescending(v => v.RegistrationDate).Skip((page - 1) * pageSize).Take(pageSize), count);
    }
}
