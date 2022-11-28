using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;
using StringHash;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.User.Commands.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserVm>>
{
    private readonly IUserService _userService;

    public LoginCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<UserVm>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _userService.GetUserAsync(request.Email, cancellationToken);
        if (response.IsSuccess)
        {
            if (response.Value.Password.VerifyHashedString(request.Password)) return Result.Success(response.Value);
            return Result.Error("Password and Email is not match");
        }
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());

    }
}
