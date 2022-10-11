using MediatR;

namespace SessionGatewayService.Application.Features.User.Commands;

public class LoginCommand : IRequest<bool>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
