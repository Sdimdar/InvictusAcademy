using Ardalis.Result;
using MediatR;
using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.Application.Features.User.Commands.Login;

public class LoginCommand : IRequest<Result<UserVm>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
