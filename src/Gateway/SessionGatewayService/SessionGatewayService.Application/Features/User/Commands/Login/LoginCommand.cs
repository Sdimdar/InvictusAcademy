using Ardalis.Result;
using MediatR;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;

namespace SessionGatewayService.Application.Features.User.Commands.Login;

public class LoginCommand : IRequest<Result<UserVm>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
