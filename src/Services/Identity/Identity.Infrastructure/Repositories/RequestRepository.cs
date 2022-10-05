using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly IdentityDbContext _context;

    public RequestRepository(IdentityDbContext context)
    {
        _context = context ?? throw new NullReferenceException(nameof(context));
    }

    public async Task<IEnumerable<Request>> GetAllRequestsAsync()
    {
        return await _context.Requests.ToListAsync();
    }

    public async Task<Request?> GetByCreatedDateAsync(DateTime date)
    {
        return await _context.Requests.FirstOrDefaultAsync(r => r.CreatedDate == date);
    }

    public async Task<Request?> GetByUserNameAsync(string username)
    {
        return await _context.Requests.FirstOrDefaultAsync(r => r.UserName == username);
    }

    public async Task<Request> CreateAsync(Request request)
    {
        request.CreatedDate = DateTime.Now;
        request.WasCalled = false;
        
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(Request request)
    {
        _context.Requests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Request? request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        if (request == null) throw new KeyNotFoundException();
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();
    }
}