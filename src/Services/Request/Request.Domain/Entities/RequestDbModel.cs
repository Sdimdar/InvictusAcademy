using CommonRepository.Models;

namespace Request.Domain.Entities;

public class RequestDbModel : BaseRepositoryEntity
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string? ManagerComment { get; set; }
    public bool WasCalled { get; set; } = false;
}