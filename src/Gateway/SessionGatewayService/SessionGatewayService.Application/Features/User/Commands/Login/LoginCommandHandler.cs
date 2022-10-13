using Ardalis.Result;
using MediatR;
using PasswordsHash;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.Application.Features.User.Commands.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserVm>>
{
    private readonly IUserService _userService;

    public LoginCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<UserVm>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var responce = await _userService.GetUserAsync(request.Email, cancellationToken);
        if (responce.IsSuccess)
        {
            if (responce.Value.Password.VerifyHashedString(request.Password)) return Result.Success();
            return Result.Error("Password and Email is not match");
        }
        if (responce.Errors.Count() != 0) return Result.Error(responce.Errors);
        return Result.Invalid(responce.ValidationErrors.ToList());

    }
}
