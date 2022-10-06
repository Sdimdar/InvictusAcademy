using System.Linq.Expressions;
using Identity.Application.Features.Requests.Queries.GetAllRequest;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllRequestsAsync();
    Task<Request?> GetByCreatedDateAsync(DateTime date);
    Task<Request?> GetByUserNameAsync(string username);
    Task<Request> CreateAsync(Request request);
    Task UpdateAsync(Request request);
    Task DeleteAsync(int id);
    Task<List<Request>> GetRequestsByPage(GetAllRequestCommand pageInfo);
}