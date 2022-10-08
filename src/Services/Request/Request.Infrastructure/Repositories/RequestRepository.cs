
using Microsoft.EntityFrameworkCore;
using Request.Application.Contracts;
using Request.Application.Features.Requests.Queries.GetAllRequest;
using Request.Infrastructure.Persistence.DbMap;

namespace Request.Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly AdminDbContext _context;

    public RequestRepository(AdminDbContext context)
    {
        _context = context ?? throw new NullReferenceException(nameof(context));
    }

    public async Task<IEnumerable<Domain.Entities.Request>> GetAllRequestsAsync()
    {
        return await _context.Requests.ToListAsync();
    }

    public async Task<Domain.Entities.Request?> GetByCreatedDateAsync(DateTime date)
    {
        return await _context.Requests.FirstOrDefaultAsync(r => r.CreatedDate == date);
    }

    public async Task<Domain.Entities.Request?> GetByUserNameAsync(string username)
    {
        return await _context.Requests.FirstOrDefaultAsync(r => r.UserName == username);
    }

    public async Task<Domain.Entities.Request> CreateAsync(Domain.Entities.Request request)
    {
        request.CreatedDate = DateTime.Now;
        request.WasCalled = false;
        
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(Domain.Entities.Request request)
    {
        _context.Requests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Domain.Entities.Request? request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        if (request == null) throw new KeyNotFoundException();
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Domain.Entities.Request>> GetRequestsByPage(GetAllRequestCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return await _context.Requests.ToListAsync();
        IQueryable<Domain.Entities.Request> requestsPerPage = _context.Requests.OrderBy(r => r.Id).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
        var result = await requestsPerPage.ToListAsync();
        return result;
    }
}