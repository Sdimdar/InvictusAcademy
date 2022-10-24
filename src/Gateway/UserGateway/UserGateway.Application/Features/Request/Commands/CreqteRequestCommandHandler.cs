using Ardalis.Result;
using MediatR;
using ServicesContracts.Request.Requests.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Request.Commands;

public class CreqteRequestCommandHandler : IRequestHandler<CreateRequestCommand, Result<string>>
{

    private readonly IRequestService _requestService;

    public CreqteRequestCommandHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<Result<string>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var response = await _requestService.CreateResponseAsync(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}
