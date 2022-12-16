using Request.Domain.Entities;

namespace ServicesContracts.Request.Responses;

public class GetAllRequestVm
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<RequestDbModel> Requests { get; set; }
    public int RequestsCount { get; set; }
    public bool HasPreviousPage
    {
        get
        {
            return (PageNumber > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageNumber < TotalPages);
        }
    }
}