using CommonRepository.Abstractions;
using Request.Domain.Entities;

namespace Request.Application.Contracts;

public interface IRequestRepository : IBaseRepository<RequestDbModel>
{
    
}