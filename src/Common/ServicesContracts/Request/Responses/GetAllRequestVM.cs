using Request.Domain.Entities;

namespace ServicesContracts.Request.Responses;

public class GetAllRequestVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<RequestDbModel> Requests { get; set; }
}