using MediatR;

namespace SessionGatewayService.Application.Features.User.Commands;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
{
    public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request.Email == "string" && request.Password == "string")
        {
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
