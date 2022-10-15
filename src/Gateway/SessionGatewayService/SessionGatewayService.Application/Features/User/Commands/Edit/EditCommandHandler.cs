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
        var responce = await _userService.EditAsync(request, cancellationToken);
        if (responce.IsSuccess) return Result.Success();
        if (responce.Errors.Count() != 0) return Result.Error(responce.Errors);
        return Result.Invalid(responce.ValidationErrors.ToList());
    }
}
