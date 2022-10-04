using Ardalis.Result;
using Identity.Application.Features.Users.Queries.GetUserData;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerry : IRequest<Result<UsersDataVm>>
{
    public int Page { get; set; }

    public GetUsersDataQuerry(int page)
    {
        Page = page;
    }
}