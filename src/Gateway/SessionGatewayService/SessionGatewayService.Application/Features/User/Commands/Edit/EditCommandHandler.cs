using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Commands;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.User.Commands.Edit;

public class EditCommandHandler : IRequestHandler<EditCommand, Result>
{
    private readonly IUserService _userService;

    public EditCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(EditCommand request, CancellationToken cancellationToken)
    {
        var response = await _userService.EditAsync(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}
