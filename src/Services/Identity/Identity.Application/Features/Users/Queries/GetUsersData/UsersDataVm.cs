using Identity.Application.Features.Users.Queries.GetUserData;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class UsersDataVm
{
    public IEnumerable<UserDataVm> Users { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
}