using System.Linq.Expressions;
using Identity.Application.Contracts;
using Identity.Application.Features.GeneralVM;
using Identity.Application.Features.Services.Abstractions;
using Identity.Application.Features.Users.Queries.GetUsersData;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;
    private readonly IUsersPaginationService _usersPaginationService;

    public UserRepository(IdentityDbContext context, IUsersPaginationService usersPaginationService)
    {
        _context = context ?? throw new NullReferenceException(nameof(context));
        _usersPaginationService = usersPaginationService;
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

    public async Task<User> GetByPredicateAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.Users.FirstOrDefaultAsync(predicate);
    }
    
    public async Task<UsersDataVm> GetPaginatedAll( int pageSize, int page)
    {
        var users = _context.Users.AsQueryable();
        var paginationData = await _usersPaginationService.GetABatchOfData(users, page, pageSize);
        UsersDataVm model = new UsersDataVm()
        {
            Users = paginationData.Item1.ToArray(),
            PageVm = new PageVm(paginationData.Item2, page, pageSize)
        };
        return model;
    }
}
