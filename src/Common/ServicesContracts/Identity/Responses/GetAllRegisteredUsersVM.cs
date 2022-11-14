using User.Domain.Entities;

namespace ServicesContracts.Identity.Responses;

public class GetAllRegisteredUsersVM
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    
    public List<UserDbModel> Users { get; set; }
    
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