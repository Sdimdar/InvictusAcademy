using Identity.Domain.Entities;

namespace Identity.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<Request> Requests { get; set; }
}