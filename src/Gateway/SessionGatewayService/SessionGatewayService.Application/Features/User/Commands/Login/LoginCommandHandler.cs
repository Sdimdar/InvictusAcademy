using Ardalis.Result;
using MediatR;
using PasswordsHash;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.Application.Contracts;

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
        var Response = await _userService.GetUserAsync(request.Email, cancellationToken);
        if (Response.IsSuccess)
        {
            if (Response.Value.Password.VerifyHashedString(request.Password)) return Result.Success();
            return Result.Error("Password and Email is not match");
        }
        if (Response.Errors.Count() != 0) return Result.Error(Response.Errors);
        return Result.Invalid(Response.ValidationErrors.ToList());

    }
}
