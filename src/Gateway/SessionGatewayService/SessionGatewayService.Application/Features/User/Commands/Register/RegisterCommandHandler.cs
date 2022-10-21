using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.User.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterVm>>
{
    private readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<RegisterVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var Response = await _userService.RegisterAsync(request, cancellationToken);
        if (Response.IsSuccess) return Result.Success();
        if (Response.Errors.Count() != 0) return Result.Error(Response.Errors);
        return Result.Invalid(Response.ValidationErrors.ToList());
    }
}
