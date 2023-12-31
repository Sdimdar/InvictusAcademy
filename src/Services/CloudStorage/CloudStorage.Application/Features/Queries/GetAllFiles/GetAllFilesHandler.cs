using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CloudStorage.Application.Contracts;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.CloudStorage.Requests.Querries;
using ServicesContracts.CloudStorage.Responses;

namespace CloudStorage.Application.Features.Queries.GetAllFiles;

public class GetAllFilesHandler : IRequestHandler<GetAllFilesQuery, Result<GetAllFilesVM>>
{
    private readonly ICloudStorageRepository _cloudStorage;
    private readonly IValidator<GetAllFilesQuery> _validator;
    private readonly ILogger<GetAllFilesHandler> _logger;

    public GetAllFilesHandler(ICloudStorageRepository cloudStorage, IValidator<GetAllFilesQuery> validator,
        ILogger<GetAllFilesHandler> logger)
    {
        _cloudStorage = cloudStorage;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<GetAllFilesVM>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());

        var usersCount = await _cloudStorage.GetCountAsync();
        if (request.PageSize == 0)
        {
            request.PageNumber = 1;
            request.PageSize = usersCount;
        }

        if (usersCount == 0)
        {
            _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
            return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
        }

        var data = await _cloudStorage.GetFilteredBatchOfData(request.PageSize, request.PageNumber, request.FilterString);
        data.ToList().ForEach(x => x.FilePath = $"https://invictus.object.pscloud.io/{x.FilePath}");
        
        var response = new GetAllFilesVM
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Files = data
        };
        return Result.Success(response);
    }
}