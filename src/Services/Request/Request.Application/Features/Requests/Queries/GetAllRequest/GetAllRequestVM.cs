namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<Domain.Entities.Request> Requests { get; set; }
}