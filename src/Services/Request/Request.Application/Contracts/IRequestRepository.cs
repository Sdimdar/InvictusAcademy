using CommonRepository.Abstractions;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Querries;

namespace Request.Application.Contracts;

public interface IRequestRepository : IBaseRepository<RequestDbModel>
{
}