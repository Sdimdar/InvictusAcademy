using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Request.Application.Contracts;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;

namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestsHandler : IRequestHandler<GetAllRequestsQuery, Result<GetAllRequestVm>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IValidator<GetAllRequestsQuery> _validator;
    private readonly ILogger<GetAllRequestsHandler> _logger;


    public GetAllRequestsHandler(IRequestRepository requestRepository, IValidator<GetAllRequestsQuery> validator, ILogger<GetAllRequestsHandler> logger)
    {
        _requestRepository = requestRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<GetAllRequestVm>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        
        var usersCount = _requestRepository.GetCountAsync();
        if (request.PageSize == 0)
        {
            request.PageNumber = 1;
            request.PageSize = await usersCount;
        }

        if (await usersCount == 0)
        {
            _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
            return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
        }

        var data = await _requestRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber);

        var response = new GetAllRequestVm
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Requests = data
        };
        return Result.Success(response);
    }
}