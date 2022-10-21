using Ardalis.Result;
using MediatR;
using ServicesContracts.Request.Requests.Commands;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.Request.Commands;

public class CreqteRequestCommandHandler : IRequestHandler<CreateRequestCommand, Result<string>>
{

    private readonly IRequestService _requestService;

    public CreqteRequestCommandHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<Result<string>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var Response = await _requestService.CreateResponseAsync(request, cancellationToken);
        if (Response.IsSuccess) return Result.Success();
        if (Response.Errors.Count() != 0) return Result.Error(Response.Errors);
        return Result.Invalid(Response.ValidationErrors.ToList());
    }
}
