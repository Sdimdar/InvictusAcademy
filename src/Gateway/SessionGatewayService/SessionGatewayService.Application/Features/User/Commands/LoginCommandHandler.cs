using MediatR;
using PasswordsHash;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.Application.Features.User.Commands;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
{
    private readonly IUserService _userService;

    public LoginCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        UserVm vm = await _userService.GetUserAsync(request.Email, cancellationToken);
        if (vm.Password.VerifyHashedString(request.Password)) return true;
        return false;
    }
}
