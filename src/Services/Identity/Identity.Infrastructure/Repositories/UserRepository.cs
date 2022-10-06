using System.Linq.Expressions;
using Identity.Application.Contracts;
using Identity.Infrastructure.Extensions;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context ?? throw new NullReferenceException(nameof(context));
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        user.RegistrationDate = DateTime.Now;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new KeyNotFoundException();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.Users.FirstOrDefaultAsync(predicate);
    }
    
    public async Task<(IEnumerable<User>, int)> GetPaginatedAll(string? filterString, int pageSize, int page)
    {
        var result = await _context.Users.Filter(filterString)
                                   .GetABatchOfData(page, pageSize);
        return (result.Item1.ToArray(), result.Item2);
        //UsersDataVm model = new()
        //{
        //    Users = paginationData.Item1.ToArray(),
        //    PageVm = new PageVm(paginationData.Item2, page, pageSize)
        //};
        //return model;
    }
}
