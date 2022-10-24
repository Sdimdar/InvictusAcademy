using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace UserGateway.Application.Features.User.Commands.Login;

public class LoginCommand : IRequest<Result<UserVm>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
