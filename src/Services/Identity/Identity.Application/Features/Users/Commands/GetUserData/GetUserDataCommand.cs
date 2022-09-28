using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Users.Commands.GetUserData;

public class GetUserDataCommand : IRequest<Result<UserDataVm>>
{
    public string Email { get; set; }

    public GetUserDataCommand(string email)
    {
        Email = email;
    }
}
