using CommonRepository.Models;

namespace Request.Domain.Entities;

public class Request:BaseRepositoryEntity
{
    public int  Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string? ManagerComment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool WasCalled { get; set; } = false;
}