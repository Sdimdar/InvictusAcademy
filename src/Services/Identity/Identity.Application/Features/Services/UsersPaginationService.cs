using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Services;

public class UsersPaginationService
{
    public async Task<(IQueryable<User>, int)> GetABatchOfData(IQueryable<User> users, int page, int pageSize)
    {
        int count = await users.CountAsync();
        return (users.OrderByDescending(v => v.RegistrationDate).Skip((page - 1) * pageSize).Take(pageSize), count);
    }
}