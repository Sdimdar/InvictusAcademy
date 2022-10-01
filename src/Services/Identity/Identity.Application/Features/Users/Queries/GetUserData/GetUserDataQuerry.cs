using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUserData;

public class GetUserDataQuerry : IRequest<Result<UserDataVm>>
{
    public string Email { get; set; }

    public GetUserDataQuerry(string email)
    {
        Email = email;
    }
}
