using Request.Domain.Entities;

namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<RequestDbModel> Requests { get; set; }
}