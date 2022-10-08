using Request.Application.Features.Requests.Queries.GetAllRequest;


namespace Request.Application.Contracts;

public interface IRequestRepository
{
    Task<IEnumerable<Domain.Entities.Request>> GetAllRequestsAsync();
    Task<Domain.Entities.Request?> GetByCreatedDateAsync(DateTime date);
    Task<Domain.Entities.Request?> GetByUserNameAsync(string username);
    Task<Domain.Entities.Request> CreateAsync(Domain.Entities.Request request);
    Task UpdateAsync(Domain.Entities.Request request);
    Task DeleteAsync(int id);
    Task<List<Domain.Entities.Request>> GetRequestsByPage(GetAllRequestCommand pageInfo);
}