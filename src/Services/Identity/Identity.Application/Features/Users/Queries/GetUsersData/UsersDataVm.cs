using Identity.Application.Features.Users.Queries.GetUserData;
using SessionGatewayService.Domain.Entities;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class UsersDataVm
{
    public IEnumerable<UserVm> Users { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
}