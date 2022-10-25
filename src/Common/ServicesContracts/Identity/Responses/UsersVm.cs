namespace ServicesContracts.Identity.Responses;

public class UsersVm
{
    public IEnumerable<UserVm> Users { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
    
    public int UsersCount { get; set; }
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