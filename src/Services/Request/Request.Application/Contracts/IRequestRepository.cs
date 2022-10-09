using CommonRepository.Abstractions;
using Request.Application.Features.Requests.Queries.GetAllRequest;
using Request.Domain.Entities;

namespace Request.Application.Contracts;

public interface IRequestRepository : IBaseRepository<RequestDbModel>
{

    Task<List<RequestDbModel>> GetRequestsByPage(GetAllRequestCommand pageInfo);
    Task<int> GetRequestsCount();
}