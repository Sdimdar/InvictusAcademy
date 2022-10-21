using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Commands;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.User.Commands.Edit;

public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, Result>
{
    private readonly IUserService _userService;

    public EditPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Result> Handle(EditPasswordCommand request, CancellationToken cancellationToken)
    {
        var Response = await _userService.EditPasswordAsync(request, cancellationToken);
        if (Response.IsSuccess) return Result.Success();
        if (Response.Errors.Count() != 0) return Result.Error(Response.Errors);
        return Result.Invalid(Response.ValidationErrors.ToList());
    }
}