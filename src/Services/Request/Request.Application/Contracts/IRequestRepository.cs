using CommonRepository.Abstractions;
using Request.Application.Features.Requests.Queries.GetAllRequest;


namespace Request.Application.Contracts;

public interface IRequestRepository:IBaseRepository<Domain.Entities.Request>
{
    
    Task<List<Domain.Entities.Request>> GetRequestsByPage(GetAllRequestCommand pageInfo);
    Task<int> GetRequestsCount();
}