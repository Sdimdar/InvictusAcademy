using System.Linq.Expressions;
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
}