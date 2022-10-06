using Identity.Application.Features.GeneralVM;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class UsersDataVm
{
    public IEnumerable<User> Users { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
}