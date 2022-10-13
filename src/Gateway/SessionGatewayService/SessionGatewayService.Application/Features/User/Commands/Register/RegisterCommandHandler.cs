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
        var responce = await _userService.RegisterAsync(request, cancellationToken);
        if (responce.IsSuccess) return Result.Success();
        if (responce.Errors.Count() != 0) return Result.Error(responce.Errors);
        return Result.Invalid(responce.ValidationErrors.ToList());
    }
}
