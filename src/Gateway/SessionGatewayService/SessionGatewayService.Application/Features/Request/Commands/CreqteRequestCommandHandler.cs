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
        var responce = await _requestService.CreateResponceAsync(request, cancellationToken);
        if (responce.IsSuccess) return Result.Success();
        if (responce.Errors.Count() != 0) return Result.Error(responce.Errors);
        return Result.Invalid(responce.ValidationErrors.ToList());
    }
}
