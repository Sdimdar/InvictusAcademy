using Identity.Application.Features.Services.Abstractions;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Services;

public class UsersFilter : IUsersFilter
{
    public IQueryable<User> Filter(IQueryable<User> users, string filterByName)
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
}