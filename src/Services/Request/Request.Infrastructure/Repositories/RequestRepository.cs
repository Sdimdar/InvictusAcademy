
using CommonRepository;
using Microsoft.EntityFrameworkCore;
using Request.Application.Contracts;
using Request.Application.Features.Requests.Queries.GetAllRequest;
using Request.Infrastructure.Persistence;
using Request.Infrastructure.Persistence.DbMap;

namespace Request.Infrastructure.Repositories;

public class RequestRepository : BaseRepository<Domain.Entities.Request,RequestDbContext>,IRequestRepository
{
   
    public RequestRepository(RequestDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<List<Domain.Entities.Request>> GetRequestsByPage(GetAllRequestCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return await _context.Requests.ToListAsync();
        IQueryable<Domain.Entities.Request> requestsPerPage = _context.Requests.OrderBy(r => r.Id).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
        var result = await requestsPerPage.ToListAsync();
        return result;
    }

    public async Task<int> GetRequestsCount()
    {
        IQueryable<Domain.Entities.Request> result =  _context.Requests;
        return  result.Count();

    }
}