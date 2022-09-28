using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
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
}
