using CommonRepository;
using Microsoft.EntityFrameworkCore;
using Request.Application.Contracts;
using Request.Domain.Entities;
using Request.Infrastructure.Persistence;
using ServicesContracts.Request.Requests.Querries;

namespace Request.Infrastructure.Repositories;

public class RequestRepository : BaseRepository<RequestDbModel, RequestDbContext>, IRequestRepository
{

    public RequestRepository(RequestDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<RequestDbModel>> GetRequestsByPage(GetAllRequestCommand pageInfo)
    {
        if (pageInfo.PageNumber == 0)
            return await _context.Requests.ToListAsync();
        IQueryable<RequestDbModel> requestsPerPage = _context.Requests.OrderBy(r => r.Id).Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
        var result = await requestsPerPage.ToListAsync();
        return result;
    }

    public async Task<int> GetRequestsCount()
    {
        IQueryable<RequestDbModel> result = _context.Requests;
        return result.Count();

    }
}