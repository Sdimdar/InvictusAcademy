using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.User.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterVm>>
{
    private readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<RegisterVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _userService.RegisterAsync(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}
