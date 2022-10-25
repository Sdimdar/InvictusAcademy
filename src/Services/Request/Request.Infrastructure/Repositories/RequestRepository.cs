using CommonRepository;
using Request.Application.Contracts;
using Request.Domain.Entities;
using Request.Infrastructure.Persistence;

namespace Request.Infrastructure.Repositories;

public class RequestRepository : BaseRepository<RequestDbModel, RequestDbContext>, IRequestRepository
{
    public RequestRepository(RequestDbContext dbContext) : base(dbContext) {}

    protected override IQueryable<RequestDbModel> FilterByString(IQueryable<RequestDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.UserName.ToLower().Contains(filterString.ToLower())
                            || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
            );
    }
}