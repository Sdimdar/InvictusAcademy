using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.User.Commands.Edit;

public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, Result>
{
    private readonly IUserService _userService;

    public EditPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(EditPasswordCommand request, CancellationToken cancellationToken)
    {
        var response = await _userService.EditPasswordAsync(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}